using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation; // Required for IClientModelValidator

namespace SportProduct.Validation
{
    /// <summary>
    /// Custom validation attribute to ensure a date is in the past.
    /// Now with client-side validation support.
    /// </summary>
    public class PastDateAttribute : ValidationAttribute, IClientModelValidator
    {
        public PastDateAttribute()
        {
            ErrorMessage = "Ngày sản xuất phải là một ngày trong quá khứ.";
        }

        // Server-side validation logic (remains the same)
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || !DateTime.TryParse(value.ToString(), out DateTime dateToValidate))
            {
                // Let the [Required] attribute handle null values.
                // If it's not a valid date, it's not our concern here.
                return ValidationResult.Success;
            }

            if (dateToValidate.Date >= DateTime.Now.Date)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }

        // Client-side validation logic
        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // Add HTML data-* attributes to the input tag.
            // These attributes will be used by our custom jQuery script.
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-pastdate", ErrorMessage);

            // Pass today's date to the client-side script in a universal format.
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            MergeAttribute(context.Attributes, "data-val-pastdate-today", today);
        }

        private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);
            return true;
        }
    }
}
