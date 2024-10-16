using System.ComponentModel.DataAnnotations;

namespace Taller1.Validation;

/// <summary>
/// This is a implementation for validate is a rut is valid
/// </summary>

[AttributeUsage(AttributeTargets.Property |
                AttributeTargets.Field, AllowMultiple = false)]
public class RutValidator : ValidationAttribute
{

    /// <summary>
    /// Check if a rut is valid
    /// </summary>
    /// <param name="rutAsObject"> The rut as object can be null </param>
    /// <returns></returns>
    
    public override bool IsValid(object? rutAsObject)
    {
        if (rutAsObject == null)
        {
            return false;
        }

        var rut = (string)rutAsObject;

        return checkIfIsARutValid(rut);
    }
    
    /// <summary>
    /// Check if a rut is valid
    /// </summary>
    /// <param name="rutAsString"> The rut as St ring </param>
    /// <returns></returns>

    public bool checkIfIsARutValid(string rutAsString)
    {
        return true;
    }
    
}