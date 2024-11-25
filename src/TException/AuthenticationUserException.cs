namespace Taller1.TException;

/// <summary>
/// This is exception fired when authentication failed 
/// </summary>

public class AuthenticationUserException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationUserException"/> class.
    /// </summary>
    public AuthenticationUserException() { }
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationUserException"/> class
    /// with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public AuthenticationUserException(string message) : base(message) {}
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationUserException"/> class
    /// with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="exception">The exception that is the cause of the current exception.</param>
    public AuthenticationUserException(string message, Exception exception) : base(message, exception) {}

}