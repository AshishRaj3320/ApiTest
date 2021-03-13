
using ApiTest.Abstraction.Models;
using ApiTest.Abstraction.Services.Enum;
using System.Threading.Tasks;

namespace ApiTest.Abstraction.Services
{
    public interface IPaymentGateway
    {
        Task<ServiceResponse> ProcessPayment(CardDetails cardDetails);
    }
}
