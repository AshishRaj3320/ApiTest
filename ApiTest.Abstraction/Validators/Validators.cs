using ApiTest.Abstraction.Request;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ApiTest.Abstraction.Validators
{
    public static class Validators
    {
        public static List<string> ValidateCardDetails(ProcessPaymentDTO cardDetails)
        {
            var listError = new List<string>();
            if (!ValidateCreditCard(cardDetails.CreditCardNumber))
            {
                listError.Add(string.Format("Not a valid credit card number : {0}.", cardDetails.CreditCardNumber));
            }
            if (!string.IsNullOrEmpty(cardDetails.SecurityCode) && cardDetails.SecurityCode.Length != 3)
            {
                listError.Add(string.Format("Not a valid security code : {0}.", cardDetails.SecurityCode));
            }
            if (cardDetails.ExpirationDate.Year < System.DateTime.Now.Year || cardDetails.ExpirationDate.Month < System.DateTime.Now.Month)
            {
                listError.Add(string.Format("Not a valid expiration date : {0}.", cardDetails.ExpirationDate));
            }
            if (cardDetails.Amount < 0)
            {
                listError.Add(string.Format("Not a valid amount : {0}.", cardDetails.Amount));
            }
            return listError;
        }

        static bool ValidateCreditCard(string creditCardNumber)
        {
            //Strip any non-numeric values
            creditCardNumber = Regex.Replace(creditCardNumber, @"[^\d]", "");

            //Build your Regular Expression
            Regex expression = new Regex(@"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$");

            //Return if it was a match or not
            return expression.IsMatch(creditCardNumber);
        }
    }
}
