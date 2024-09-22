using Taller1.Model;
using Taller1.Service;
using Taller1.Util;

namespace Taller1.Authenticate;

public class DefaultRegistrationHandler : IRegistrationHandler
{

    private readonly Random _random = new Random();
    private readonly IObjectService<User> _userService;
    private readonly IEncryptService _encryptService;

    public DefaultRegistrationHandler(IObjectService<User> userService,
        IEncryptService encryptService)
    {
        _userService = userService;
        _encryptService = encryptService;
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

        var passwordEncrypt = _encryptService.Encrypt(password);
        
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
        
        _userService.Push(user);
        return RegistrationResponse.
            Success("Registered");
    }
    
}