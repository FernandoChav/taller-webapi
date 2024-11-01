using Microsoft.EntityFrameworkCore;
using Taller1.Data;
using Taller1.Mapper;
using Taller1.Model;
using Taller1.Search;
using Taller1.Service;

namespace Taller1.Authenticate;

public class DefaultRegistrationHandler(
    IObjectRepository<User> userRepository,
    ApplicationDbContext applicationDbContext,
    IMapperFactory mapperFactory
    ) : IRegistrationHandler
{

    private readonly DbSet<User> _users = 
        applicationDbContext.Users;

    private readonly IObjectMapper<UserCreation, User> _toUserMapper =
        mapperFactory.Get<UserCreation, User>();
    
    public RegistrationResponse Register(UserCreation userCreation)
    {
        
        var password = userCreation.Password;
        var repeatPassword = userCreation.RepeatPassword;

        if (password != repeatPassword)
        {
            return RegistrationResponse.
                Error("The password is not the same");
        }

        var userSearched = DbSearchBuilder<User>.NewBuilder(_users)
            .Filter(userSearched => userCreation.Rut == userSearched.Rut)
            .BuildAndGetFirst();

        if (userSearched != null)
        {
            return RegistrationResponse.Error(
                "The user already exists");
        }
        
        var user = _toUserMapper.Mapper(userCreation);
        
        userRepository.Push(user);
        return RegistrationResponse.
            Success("Ok");
    }
    
}