namespace Taller1.TException;

/// <summary>
/// This is exception fired when authentication failed 
/// </summary>

public class AuthenticationUserException : Exception
{

    public AuthenticationUserException() { }

    public AuthenticationUserException(string message) : base(message) {}

    public AuthenticationUserException(string message, Exception exception) : base(message, exception) {}

}