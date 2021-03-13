
using ApiTest.Abstraction.Models;
using ApiTest.Abstraction.Services.Enum;
using System.Threading.Tasks;

namespace ApiTest.Abstraction.Services
{
    public interface IPaymentGateway
    {
        Task<bool> ProcessPayment(CardDetails cardDetails);

        bool IsAvailable { get; set; }
        bool IsProcessed { get; set; }
    }
}
