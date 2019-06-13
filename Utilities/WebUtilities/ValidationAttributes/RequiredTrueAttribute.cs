using System;
using System.ComponentModel.DataAnnotations;

namespace AIT.WebUtilities.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class RequiredTrueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return value is bool && (bool)value ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }
    }
}
