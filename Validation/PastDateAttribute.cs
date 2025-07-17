using System;
using System.ComponentModel.DataAnnotations;

namespace SportProduct.Validation // Thay YourProjectName bằng tên dự án của bạn
{
    /// <summary>
    /// Custom validation attribute to ensure a date is in the past.
    /// </summary>
    public class PastDateAttribute : ValidationAttribute
    {
        public PastDateAttribute()
        {
            // Set a default error message.
            ErrorMessage = "Ngày sản xuất phải là một ngày trong quá khứ.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // The value is not required, another attribute should handle that.
            // If no value, it's considered valid here.
            if (value == null)
            {
                return ValidationResult.Success;
            }

            // Attempt to convert the value to a DateTime.
            if (DateTime.TryParse(value.ToString(), out DateTime dateToValidate))
            {
                // Check if the date is in the future.
                // We compare only the date part, ignoring the time.
                if (dateToValidate.Date >= DateTime.Now.Date)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            else
            {
                // If conversion fails, return an error.
                return new ValidationResult("Định dạng ngày không hợp lệ.");
            }

            // If all checks pass, the date is valid.
            return ValidationResult.Success;
        }
    }
}
