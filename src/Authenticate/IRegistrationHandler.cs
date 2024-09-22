using Taller1.Model;

namespace Taller1.Authenticate;

public interface IRegistrationHandler
{

    RegistrationResponse Register(UserCreation userCreation);

}