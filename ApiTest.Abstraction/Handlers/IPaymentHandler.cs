
using ApiTest.Abstraction.Models;
using System.Threading.Tasks;

namespace ApiTest.Abstraction.Handlers
{
   public interface IPaymentHandler
    {
        Task<bool> ProcessPayment(CardDetails cardDetails);
    }
}
