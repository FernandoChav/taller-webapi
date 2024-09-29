using Taller1.Model;
using Taller1.Service;
using Taller1.Util;

namespace Taller1.Authenticate;

public class DefaultRegistrationHandler : IRegistrationHandler
{

    private readonly Random _random = new Random();
    private readonly IObjectRepository<User> _userRepository;
    private readonly IEncryptStrategy _encryptStrategy;

    public DefaultRegistrationHandler(IObjectRepository<User> userRepository,
        IEncryptStrategy encryptStrategy)
    {
        _userRepository = userRepository;
        _encryptStrategy = encryptStrategy;
    }
    
    public RegistrationResponse Register(UserCreation userCreation)
    {
        var password = userCreation.Password;
        var repeatPassword = userCreation.RepeatPassword;

        if (password != repeatPassword)
        {
            return RegistrationResponse.
                Error("The password is not same");
        }

        var passwordEncrypt = _encryptStrategy.Encrypt(password);
        
        var user = new User
        {
            Name = userCreation.Name,
            Rut = userCreation.Rut,
            Gender = userCreation.GenderType,
            Password = passwordEncrypt,
            Email = userCreation.Email,
            Id = _random.Next(1,500),
            RoleId = 0
        };
        
        _userRepository.Push(user);
        return RegistrationResponse.
            Success("Registered");
    }
    
}