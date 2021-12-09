using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ev.Repositories
{
    public class ValidateOrderDate: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime currentDate = DateTime.Now;
            string message = string.Empty;
            if (Convert.ToDateTime(value).Date >= currentDate.Date)
            {
                return ValidationResult.Success;
            }
            else
            {
                message = "Join date cannot be less than current date";
                return new ValidationResult(message);
            }
        }
    }
}