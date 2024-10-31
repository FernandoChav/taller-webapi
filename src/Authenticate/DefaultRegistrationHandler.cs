using Microsoft.EntityFrameworkCore;
using Taller1.Data;
using Taller1.Model;
using Taller1.Search;
using Taller1.Service;
using Taller1.Util;

namespace Taller1.Authenticate;

public class DefaultRegistrationHandler(
    IObjectRepository<User> userRepository,
    ApplicationDbContext applicationDbContext,
    IEncryptStrategy encryptStrategy
    ) : IRegistrationHandler
{

    private DbSet<User> _users = 
        applicationDbContext.Users;
    
    public RegistrationResponse Register(UserCreation userCreation)
    {
        var password = userCreation.Password;
        var repeatPassword = userCreation.RepeatPassword;

        if (password != repeatPassword)
        {
            return RegistrationResponse.
                Error("The password is not the same");
        }

        var userSearched = DbSetSearchBuilder<User>.NewBuilder(_users)
            .Filter(userSearched => userCreation.Rut == userSearched.Rut)
            .BuildAndGetFirst();

        if (userSearched != null)
        {
            return RegistrationResponse.Error(
                "The user already exists");
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