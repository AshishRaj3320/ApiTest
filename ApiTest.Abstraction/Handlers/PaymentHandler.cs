
using ApiTest.Abstraction.Models;
using ApiTest.Abstraction.Services;
using ApiTest.Abstraction.Services.Enum;
using ApiTest.SQL;
using ApiTest.SQL.DBModels;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace ApiTest.Abstraction.Handlers
{
    public class PaymentHandler : IPaymentHandler
    {
        private readonly IEntityRepository<Payment> _entityPaymentRepository;
        readonly private Func<ServiceEnum, IPaymentGateway> _serviceResolver;
        private readonly IMapper _mapper;
        public PaymentHandler(Func<ServiceEnum, IPaymentGateway> serviceResolver, IEntityRepository<Payment> entityPaymentRepository, IMapper mapper)
        {
            _serviceResolver = serviceResolver;
            _entityPaymentRepository = entityPaymentRepository;
            _mapper = mapper;
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
            var paymentTransction = await AddPaymentTransaction(cardDetails, "Expensive");

            var service = _serviceResolver(ServiceEnum.Expensive);
            var response = await service.ProcessPayment(cardDetails);
            if (!response.IsAvailable)
            {
                await ProcessCheapPayment(cardDetails);
            }
            await UpdatePaymentTransaction(paymentTransction, "Expensive", response.Status);
            return true;
        }

        private async Task<bool> ProcessCheapPayment(CardDetails cardDetails)
        {
            var result = await AddPaymentTransaction(cardDetails, "Cheap");
            var service = _serviceResolver(ServiceEnum.Cheap);
            var response = await service.ProcessPayment(cardDetails);
            await UpdatePaymentTransaction(result, "Cheap", response.Status);
            return true;
        }

        private async Task<bool> ProcessPremiumPayment(CardDetails cardDetails)
        {
            bool flagloop = false;
            var transctionResult = await AddPaymentTransaction(cardDetails, "Premium");

            var service = _serviceResolver(ServiceEnum.Premium);
            var result = await service.ProcessPayment(cardDetails);
            if (!result.IsProcessed)
            {
                for (int count = 0; count <= 2; count++)
                {
                    var loopResult = await service.ProcessPayment(cardDetails);
                    if (loopResult.IsProcessed)
                    {
                        flagloop = true;
                        await UpdatePaymentTransaction(transctionResult, "Premium", loopResult.Status);
                        break;
                    }

                }
            }

            if (!flagloop)
                await UpdatePaymentTransaction(transctionResult, "Premium", result.Status);

            return true;
        }

        private async Task<Payment> AddPaymentTransaction(CardDetails cardDetails, string gateway)
        {
            var paymentTransction = _mapper.Map<Payment>(cardDetails);
            paymentTransction.Status = "pending";
            paymentTransction.GateWayType = gateway;
            return await _entityPaymentRepository.AddAsync(paymentTransction);
        }

        private async Task<Payment> UpdatePaymentTransaction(Payment paymentTransction, string gateway, string status)
        {
            paymentTransction.GateWayType = gateway;
            paymentTransction.Status = status;
            return await _entityPaymentRepository.UpdateAsync(paymentTransction);
        }
    }
}
