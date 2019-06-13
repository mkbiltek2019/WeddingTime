using AIT.WebUtilities.ValidationAttributes.Enum;
using System.Web.Mvc;

namespace AIT.WebUtilities.ValidationAttributes.ValidationRules
{
    public class ModelClientValidationRequiredIfRule : ModelClientValidationRule
    {
        public ModelClientValidationRequiredIfRule(string errorMessage, string dependentProperty, RequiredIfComparison comparison, object value, bool allowMultiple)
        {           
            ErrorMessage = errorMessage;
            ValidationType = "requiredif";
            ValidationParameters.Add("dependentprop", dependentProperty);
            ValidationParameters.Add("comparison", comparison.ToString().ToLower());
            ValidationParameters.Add("value", value.ToString());
            ValidationParameters.Add("allowmultiple", allowMultiple.ToString().ToLower());
        }
    }
}
