using System.ComponentModel.DataAnnotations;

namespace Taller1.Validation;

[AttributeUsage(AttributeTargets.Property |
                AttributeTargets.Field, AllowMultiple = false)]
public class RutValidator : ValidationAttribute
{

    public override bool IsValid(object? rutAsObject)
    {
        if (rutAsObject == null)
        {
            return false;
        }

        var rut = (string)rutAsObject;

        return checkIfIsARutValid(rut);
    }

    public bool checkIfIsARutValid(string rutAsString)
    {
        return true;
    }
    
}