using System;
using System.ComponentModel.DataAnnotations;

namespace Contract.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Property)]

    public class NonEmptyGuidAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((value is Guid) && Guid.Empty == (Guid)value)
            {
                return new ValidationResult("Guid cannot be empty.");
            }
            return null;
        }
    }
}