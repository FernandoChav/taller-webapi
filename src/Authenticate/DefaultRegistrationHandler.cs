using Taller1.Model;
using Taller1.Service;
using Taller1.Util;

namespace Taller1.Authenticate;

public class DefaultRegistrationHandler(
    IObjectRepository<User> userRepository,
    IEncryptStrategy encryptStrategy
    ) : IRegistrationHandler
{

    private readonly Random _random = new Random();

    
    public RegistrationResponse Register(UserCreation userCreation)
    {
        var password = userCreation.Password;
        var repeatPassword = userCreation.RepeatPassword;

        if (password != repeatPassword)
        {
            return RegistrationResponse.
                Error("The password is not the same");
        }

        var passwordEncrypt = encryptStrategy.Encrypt(password);
        
        var user = new User
        {
            Name = userCreation.Name,
            Rut = userCreation.Rut,
            Gender = userCreation.GenderType,
            Password = passwordEncrypt,
            Email = userCreation.Email,
            RoleId = 0
        };
        
        userRepository.Push(user);
        return RegistrationResponse.
            Success("Registered");
    }
    
}