using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class MinOf18Years:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer=(Customer)validationContext.ObjectInstance;
            if (customer.MembershipTypeId==MembershipType.Unknown
                ||customer.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;
            if (customer.BirthDate == null)
                return new ValidationResult("BirthDate field is required");
            var age = DateTime.Today.Year - customer.BirthDate.Value.Year;
            return (age >= 18) 
                ? ValidationResult.Success 
                : new ValidationResult("Customer must be older than 18 to view others");
        }
    }
}