using ApiTest.Abstraction.Models;
using ApiTest.Abstraction.Services.Enum;
using ApiTest.SQL;
using ApiTest.SQL.DBModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiTest.Abstraction.Services
{
    public class ExpensivePaymentGateway : IPaymentGateway
    {
        private bool _isavailable, _isprocessed;
        public ExpensivePaymentGateway(IEntityRepository<Payment> paymentRepository, IMapper mapper)
        {
            _isavailable = (new Random().Next(100) <= 20 ? true : false);
            _isprocessed = (new Random().Next(10) <= 20 ? true : false);
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
