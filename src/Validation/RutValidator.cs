using System.ComponentModel.DataAnnotations;

namespace Taller1.Validation;

/// <summary>
/// This is a implementation for validate is a rut is valid
/// </summary>
[AttributeUsage(AttributeTargets.Property |
                AttributeTargets.Field, AllowMultiple = false)]
public class RutValidator : ValidationAttribute
{
    private const char SeparatorRut = '-';

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
        return CheckIfIsARutValid(rut);
    }

    /// <summary>
    /// Check if a rut is valid
    /// </summary>
    /// <param name="rutAsString"> The rut as St ring </param>
    /// <returns></returns>
    private bool CheckIfIsARutValid(string rutAsString)
    {
        var rutSplit = rutAsString.Split(SeparatorRut);
        if (rutSplit.Length == 1)
        {
            return false;
        }

        var numbersAsString = rutSplit[0];
   
        if (!int.TryParse(rutSplit[0], out var numbersRut))
        {
            return false;
        }

        if (!int.TryParse(rutSplit[1], out var lastDigitAsString))
        {
            return false;
        }

        var lastDigit  = RutHelper.CalculateLastDigit(numbersAsString);
        return lastDigitAsString == lastDigit;
    }
}