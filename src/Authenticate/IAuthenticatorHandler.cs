namespace Taller1.Authenticate;

/// <summary>
/// This interface perms handle all authenticate in system
/// </summary>

public interface IAuthenticatorHandler
{

    /// <summary>
    /// Generate authentication based in a set credentials
    /// for generate access session token
    /// </summary>
    /// <param name="credentials">A set credentials for access</param>
    /// <returns>a string with contains authentication data</returns>

    Model.AuthenticationResponse Authenticate(Credentials credentials);

}