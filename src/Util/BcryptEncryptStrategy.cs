namespace Taller1.Util;

/// <summary>
/// Implements password encryption and verification using the BCrypt algorithm.
/// </summary>
public class BcryptEncryptStrategy : IEncryptStrategy 
{
    /// <summary>
    /// Encrypts the given password using BCrypt hashing algorithm.
    /// </summary>
    /// <param name="password">The password to encrypt.</param>
    /// <returns>A hashed version of the password.</returns>
    /// <exception cref="Exception">Thrown if the password is empty.</exception>
    public string Encrypt(string password)
    {
        if (password.Length == 0)
        {
            throw new Exception("The password is empty");
        }
        
        return BCrypt.Net.BCrypt.HashPassword(password);
}

    /// <summary>
    /// Verifies whether the entered password matches the hashed password.
    /// </summary>
    /// <param name="passwordEntered">The password entered by the user.</param>
    /// <param name="passwordEncrypt">The previously hashed password.</param>
    /// <returns>True if the password entered matches the hashed password; otherwise, false.</returns>
    /// <exception cref="Exception">Thrown if the entered or hashed password is empty.</exception>
    public bool Verify(string passwordEntered, string passwordEncrypt)
    {
        if (passwordEncrypt.Length == 0 || passwordEntered.Length == 0)
        {
            throw new Exception("The password is empty");
        }
        
        return BCrypt.Net.BCrypt.Verify(passwordEntered, passwordEncrypt);
    }
    
}