
using ApiTest.Abstraction.Models;
using ApiTest.Abstraction.Services;
using ApiTest.Abstraction.Services.Enum;
using System;
using System.Threading.Tasks;

namespace ApiTest.Abstraction.Handlers
{
    public class PaymentHandler : IPaymentHandler
    {
        readonly private Func<ServiceEnum, IPaymentGateway> _serviceResolver;
        public PaymentHandler(Func<ServiceEnum, IPaymentGateway> serviceResolver)
        {
            _serviceResolver = serviceResolver;
        }
        public async Task<bool> ProcessPayment(CardDetails cardDetails)
        {
            if (cardDetails.Amount <= 20)
            {
                return await ProcessCheapPayment(cardDetails);
            }
            else if (cardDetails.Amount > 20 && cardDetails.Amount <= 500)
            {
                return await ProcessExpensivePayment(cardDetails);
            }
            else
            {
                return await ProcessPremiumPayment(cardDetails);
            }
        }

        private async Task<bool> ProcessExpensivePayment(CardDetails cardDetails)
        {
            var service = _serviceResolver(ServiceEnum.Expensive);
            if (!service.IsAvailable)
            {
                service = _serviceResolver(ServiceEnum.Cheap);
                return await service.ProcessPayment(cardDetails);
            }
            return await service.ProcessPayment(cardDetails);
        }

        private async Task<bool> ProcessCheapPayment(CardDetails cardDetails)
        {
            var service = _serviceResolver(ServiceEnum.Cheap);
            return await service.ProcessPayment(cardDetails);
        }

        private async Task<bool> ProcessPremiumPayment(CardDetails cardDetails)
        {
            var loopResult = true;
            var service = _serviceResolver(ServiceEnum.Premium);
            var result = await service.ProcessPayment(cardDetails);
            if (!result)
            {
                for (int count = 0; count <= 2; count++)
                {
                    loopResult = await service.ProcessPayment(cardDetails);
                    if (loopResult)
                    { break; }

                }
            }
            return loopResult;
        }
    }
}
