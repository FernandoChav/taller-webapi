namespace Taller1.Authenticate;

public enum RegistrationResponseStatus
{
    Successful,
    Error
}

public class RegistrationResponse
{
    private readonly string _message;
    private readonly RegistrationResponseStatus _status;

    public RegistrationResponse(string message,
        RegistrationResponseStatus status)
    {
        _message = message;
        _status = status;
    }

    public string GetMessage()
    {
        return _message;
    }

    public RegistrationResponseStatus GetStatus()
    {
        return _status;
    }

    public bool IsSuccessful()
    {
        return _status == RegistrationResponseStatus.Successful;
    }

    public static RegistrationResponse Success(string message)
    {
        return new RegistrationResponse(message,
            RegistrationResponseStatus.Successful);
    }

    public static RegistrationResponse Error(string message)
    {
        return new RegistrationResponse(message,
            RegistrationResponseStatus.Error);
    }
}