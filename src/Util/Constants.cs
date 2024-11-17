namespace Taller1.Util;

/// <summary>
/// A set of constants 
/// </summary>

public class Constants
{

    public const string NamePattern = @"^[a-zA-ZáéíóúÁÉÍÓÚ\\s]+$";
    public const string EmailPattern = @"^(?=.*[a-zA-Z])(?=.*\d)[A-Za-z\d]+$";
    public const string PasswordPattern = @"^(?=.*[a-zA-Z])(?=.*\d)[A-Za-z\d]+$";
    public const string EmptyString = "";

    private Constants()
    {
        
    }

}