using ApiTest.Abstraction.Models;
using ApiTest.Abstraction.Services.Enum;
using ApiTest.SQL;
using ApiTest.SQL.DBModels;
using AutoMapper;
using System.Threading.Tasks;

namespace ApiTest.Abstraction.Services
{
    public class CheapPaymentGateway : IPaymentGateway
    {
        private bool _isavailable,_isprocessed;

        public CheapPaymentGateway(IEntityRepository<Payment> paymentRepository, IMapper mapper)
        {
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

        public async Task<ServiceResponse> ProcessPayment(CardDetails cardDetails)
        {
            return new ServiceResponse
            {
                IsAvailable = _isavailable,
                IsProcessed = _isprocessed,
                Status = _isprocessed && _isavailable ? "processed" : "failed"
            };
        }
    }
}
