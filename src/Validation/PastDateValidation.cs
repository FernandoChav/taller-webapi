using System.ComponentModel.DataAnnotations;

namespace Taller1.Validation;

[AttributeUsage(AttributeTargets.Property |
                AttributeTargets.Field, AllowMultiple = false)]
public class PastDateValidation : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return false;
        }
        
        var date = (DateTime) value;
        return date < DateTime.Now;
    }

    public override string FormatErrorMessage(string name)
    {
        return $"Error: {name}";
    }
    
    
}