using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StocksApp.Core.Domain.Entities.CustomValidators
{
    public class DateRangeValidatorAttribute : ValidationAttribute
    {
        public DateTime MinDate { get; set; } = Convert.ToDateTime("01-01-2000");
        public string DefaultErrorMessage { get; set; } = "Date should not older than {0}";

        public DateRangeValidatorAttribute()
        {

        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                // Get Date
                DateTime toDate = Convert.ToDateTime(value);

                if (MinDate > toDate)
                {
                    return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, MinDate));
                }
                else 
                {
                    return ValidationResult.Success;
                }
            }

            return null;
        } 
    }
}