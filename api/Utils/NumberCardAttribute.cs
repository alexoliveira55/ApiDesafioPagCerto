using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace api.Utils
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class NumberCardAttribute: ValidationAttribute
    {
        private const string MessageDefault = "Number card is invalid!";  

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return null;

            if( value.ToString().Contains(" ")) return new ValidationResult(MessageDefault);

            if (!value.ToString().All(char.IsDigit)) return new ValidationResult(MessageDefault);

            if (value.ToString().Length != 16) return new ValidationResult(MessageDefault);

            return null;

        }

        public override string FormatErrorMessage(string name)
        {
            return name;
        }
    }
}
