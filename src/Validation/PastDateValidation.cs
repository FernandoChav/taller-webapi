using System.ComponentModel.DataAnnotations;

namespace Taller1.Validation;

/// <summary>
/// This is implementation for validate if a date is past that this actual date
/// </summary>

[AttributeUsage(AttributeTargets.Property |
                AttributeTargets.Field, AllowMultiple = false)]
public class PastDateValidation : ValidationAttribute
{
    
    /// <summary>
    /// Check if a date is elder that actual
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    
    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return false;
        }
        
        var date = (DateTime) value;
        return date < DateTime.Now;
    }

    /// <summary>
    /// Show a message error
    /// </summary>
    /// <param name="name">Name attribute</param>
    /// <returns></returns>
    
    public override string FormatErrorMessage(string name)
    {
        return $"Error: {name}";
    }
    
    
}