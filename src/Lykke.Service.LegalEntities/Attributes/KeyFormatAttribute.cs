using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Lykke.Service.LegalEntities.Attributes
{
    public class KeyFormatAttribute : ValidationAttribute
    {
        private static readonly Regex RegexId = new Regex("^[A-Za-z][A-Za-z0-9\\s]{2,62}$");

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string @string = value as string;
            
            if (string.IsNullOrEmpty(@string) || !RegexId.IsMatch(@string))
                return new ValidationResult(GetErrorMessage(validationContext));

            return ValidationResult.Success;
        }

        private string GetErrorMessage(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
                return ErrorMessage;

            return $"{validationContext.DisplayName} invalid key value";
        }
    }
}
