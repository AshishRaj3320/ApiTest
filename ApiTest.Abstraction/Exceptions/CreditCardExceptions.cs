using System;

namespace ApiTest.Abstraction.Exceptions
{
    public class CreditCardExceptions : Exception
    {
        public CreditCardExceptions(string message):base(message)
        {
        }
    }
}
