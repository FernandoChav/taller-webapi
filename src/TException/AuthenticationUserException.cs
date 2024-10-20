namespace Taller1.TException;

public class AuthenticationUserException : Exception
{

    public AuthenticationUserException() { }

    public AuthenticationUserException(string message) : base(message) {}

    public AuthenticationUserException(string message, Exception exception) : base(message, exception) {}

}