using System.ComponentModel.DataAnnotations;
namespace MadLibs;

public class NotEqualAttribute : ValidationAttribute
{
    private readonly string otherProperty;

    public NotEqualAttribute(string otherProperty)
    {
        this.otherProperty = otherProperty;
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var otherPropertyInfo = validationContext.ObjectType.GetProperty(otherProperty);
        if (otherPropertyInfo == null)
        {
            return new ValidationResult($"Property with name {otherProperty} not found.");
        }

        var otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance);
        if (Equals(value, otherPropertyValue) || value == otherPropertyValue)
        {
            if (ErrorMessage != null)
            {
                return new ValidationResult(ErrorMessage);
            }
            
            return new ValidationResult($"{validationContext.DisplayName} should not be equal to {otherProperty}.");
        }

        return ValidationResult.Success!;
    }
}

