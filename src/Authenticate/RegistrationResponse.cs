namespace Taller1.Authenticate;
/// <summary>
/// Represents the status of a user registration response.
/// </summary>
public enum RegistrationResponseStatus
{
    Successful,
    Error
}
/// <summary>
/// Represents the response of a user registration operation.
/// Contains a status (success or error) and a message explaining the result.
/// </summary>
public class RegistrationResponse
{
    private readonly string _message;
    private readonly RegistrationResponseStatus _status;

    /// <summary>
    /// Initializes a new instance of the <see cref="RegistrationResponse"/> class.
    /// </summary>
    /// <param name="message">The message that provides additional information about the registration result.</param>
    public RegistrationResponse(string message,
        RegistrationResponseStatus status)
    {
        _message = message;
        _status = status;
    }

    /// <summary>
    /// Gets the message associated with the registration response.
    /// </summary>
    /// <returns>The message providing additional information about the result.</returns>
    public string GetMessage()
    {
        return _message;
    }
    /// <summary>
    /// Gets the status of the registration response (successful or error).
    /// </summary>
    /// <returns>The status of the registration response.</returns>
    public RegistrationResponseStatus GetStatus()
    {
        return _status;
    }

    /// <summary>
    /// Determines whether the registration was successful.
    /// </summary>
    /// <returns><c>true</c> if the registration was successful; otherwise, <c>false</c>.</returns>
    public bool IsSuccessful()
    {
        return _status == RegistrationResponseStatus.Successful;
    }

    /// <summary>
    /// Creates a successful registration response.
    /// </summary>
    /// <param name="message">The message describing the success.</param>
    /// <returns>A <see cref="RegistrationResponse"/> with a successful status.</returns>
    public static RegistrationResponse Success(string message)
    {
        return new RegistrationResponse(message,
            RegistrationResponseStatus.Successful);
    }
    /// <summary>
    /// Creates a failed registration response.
    /// </summary>
    /// <param name="message">The message describing the error.</param>
    /// <returns>A <see cref="RegistrationResponse"/> with an error status.</returns>
    public static RegistrationResponse Error(string message)
    {
        return new RegistrationResponse(message,
            RegistrationResponseStatus.Error);
    }
}