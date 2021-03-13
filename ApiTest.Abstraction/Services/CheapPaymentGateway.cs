using ApiTest.Abstraction.Models;
using ApiTest.SQL;
using ApiTest.SQL.DBModels;
using AutoMapper;
using System.Threading.Tasks;

namespace ApiTest.Abstraction.Services
{
    public class CheapPaymentGateway : IPaymentGateway
    {
        private readonly IEntityRepository<Payment> _paymentRepository;
        private readonly IMapper _mapper;
        private bool _isavailable,_isprocessed;

        public CheapPaymentGateway(IEntityRepository<Payment> paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _isavailable = true;
            _isprocessed = true;
        }
       
        public bool IsAvailable  // read-write instance property
        {
            get => _isavailable;
            set => _isavailable = value;
        }
        
        public bool IsProcessed  // read-write instance property
        {
            get => _isprocessed;
            set => _isprocessed = value;
        }

        public async Task<bool> ProcessPayment(CardDetails cardDetails)
        {

            var payment = _mapper.Map<Payment>(cardDetails);

            payment.Status = "pending";
            payment.GateWayType = "Cheap";

            await _paymentRepository.AddAsync(payment);

            return _isprocessed;
        }
    }
}
