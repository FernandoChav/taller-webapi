namespace Taller1.Authenticate;

public interface IAuthenticatorHandler
{

    string Authenticate(Credentials credentials);

}