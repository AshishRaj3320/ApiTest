using ApiTest.Abstraction.Errors;
using ApiTest.Abstraction.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ApiTest.Abstraction.Models
{
    public class CardDetails
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

        public CardDetails(string creditCardNumber, string cardHolder, DateTime expirationDate, string securityCode, decimal amount)
        {
            CreditCardNumber = creditCardNumber;
            CardHolder = cardHolder;
            ExpirationDate = expirationDate;
            SecurityCode = securityCode;
            Amount = amount;
        }
       
    }
}
