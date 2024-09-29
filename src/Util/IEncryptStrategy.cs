namespace Taller1.Util;

/// <summary>
/// This interface provide a way for
/// encrypt a string content
/// </summary>

public interface IEncryptStrategy
{

    /// <summary>
    /// Encrypt a string using any encrypt method
    /// </summary>
    /// <param name="password">The string content for encrypt</param>
    /// <returns>A string encrypted</returns>
    
    string Encrypt(string password);

    /// <summary>
    /// Check if two string are equals
    /// </summary>
    /// <param name="passwordEntered">A content string</param>
    /// <param name="passwordEncrypt">A content string encrypted</param>
    /// <returns>A boolean that is true if the password are equals</returns>
    
    bool Verify(string passwordEntered,
        string passwordEncrypt);

}