using System;
using System.ComponentModel.DataAnnotations;

namespace ApiTest.Abstraction.Request
{
    public class ProcessPaymentDTO
    {
        [Required]
        public string CreditCardNumber { get; set; }

        [Required]
        public string CardHolder { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        public string SecurityCode { get; set; }

        [Required]
        public decimal Amount { get; set; }

    }
}
