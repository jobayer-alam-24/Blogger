using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Blogger.Helpers.CustomAttributes
{
    public class DateNotInFutureAttribute : ValidationAttribute
    {
        public DateNotInFutureAttribute()
        {
            ErrorMessage = "Invalid Date! Date Can not be In Future!";
        }
        /* protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (value is DateTime dateValue)
            {
                if (dateValue > DateTime.Now)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            else
            {
                return new ValidationResult("Invalid Data Type!");
            }
            return ValidationResult.Success;

        }
        */
    }

}