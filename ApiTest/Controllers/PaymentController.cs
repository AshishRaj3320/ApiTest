using ApiTest.Abstraction.Errors;
using ApiTest.Abstraction.Handlers;
using ApiTest.Abstraction.Models;
using ApiTest.Abstraction.Request;
using ApiTest.Abstraction.Validators;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiTest.Controllers
{
    public class PaymentController : BaseApiController
    {
        private readonly IPaymentHandler _paymentHandler;
        private readonly IMapper _mapper;
        public PaymentController(IPaymentHandler paymentHandler, IMapper mapper)
        {
            _paymentHandler = paymentHandler;
            _mapper = mapper;
        }


        [HttpPost("processpayment")]
        public async Task<IActionResult> ProcessPayment([FromBody] ProcessPaymentDTO request)
        {
            var validateCardDetails = Validators.ValidateCardDetails(request);
            if (validateCardDetails.Count > 0)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse
                {
                    StatusCode = 404,
                    Message = "Validation failed",
                    Errors = (IEnumerable<string>)validateCardDetails
                });
            }
            
            var cardDetails = _mapper.Map<CardDetails>(request);
            var result =await _paymentHandler.ProcessPayment(cardDetails);
            return Ok(result);

        }
    }
}
