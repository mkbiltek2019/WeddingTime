using AIT.WebUtilities.ValidationAttributes.Enum;
using AIT.WebUtilities.ValidationAttributes.ValidationRules;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AIT.WebUtilities.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class RequiredIfAttribute : ValidationAttribute, IClientValidatable
    {
        public string _dependentProperty;
        public RequiredIfComparison _comparison;
        public object _value;

        public RequiredIfAttribute(string dependentProperty, RequiredIfComparison comparison, object value)
        {
            if (string.IsNullOrEmpty(dependentProperty))
            {
                throw new ArgumentNullException("dependentProperty");
            }

            _dependentProperty = dependentProperty;
            _comparison = comparison;
            _value = value;
        }

        public bool AllowMultiple { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                var property = validationContext.ObjectInstance.GetType().GetProperty(_dependentProperty);

                if (property != null)
                {
                    var propertyValue = property.GetValue(validationContext.ObjectInstance, null);

                    if (propertyValue != null)
                    {
                        if ((_comparison == RequiredIfComparison.IsEqualTo && propertyValue.Equals(_value)) || (_comparison == RequiredIfComparison.IsNotEqualTo && !propertyValue.Equals(_value)))
                            return new ValidationResult(ErrorMessage);
                    }
                }
            }
            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            return new[]
            {
                new ModelClientValidationRequiredIfRule(ErrorMessage, _dependentProperty, _comparison, _value, AllowMultiple)
            };
        }
    }
}
