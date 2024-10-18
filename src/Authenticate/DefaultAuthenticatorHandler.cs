using Microsoft.EntityFrameworkCore;
using Taller1.Authenticate.Token;
using Taller1.Data;
using Taller1.Model;
using Taller1.Search;
using Taller1.Service;
using Taller1.Util;

namespace Taller1.Authenticate;

public class DefaultAuthenticatorHandler : IAuthenticatorHandler
{
    private readonly DbSet<User> _users;
    private readonly IEncryptStrategy _encryptStrategy;
    private readonly IUserTokenProvider _tokenProvider;

    public DefaultAuthenticatorHandler(ApplicationDbContext applicationDbContext,
        IEncryptStrategy encryptStrategy,
        IUserTokenProvider tokenProvider)
    {
        _users = applicationDbContext.Users;
        _encryptStrategy = encryptStrategy;
        _tokenProvider = tokenProvider;
    }

    public string Authenticate(Credentials credentials)
    {
        var email = credentials.Email();
        var password = credentials.Password();

        var userSelected = DbSetSearchBuilder<User>
            .NewBuilder(_users)
            .Filter(user => user.Email == email)
            .BuildAndGetFirst();

        if (userSelected is null)
        {
            throw new Exception("User is null");
        }

        if (!userSelected.IsActive)
        {
            throw new Exception("The user is not active");
        }

        if (!_encryptStrategy.Verify(password, userSelected.Password))
        {
            throw new UnauthorizedAccessException("Password is not invalid");
        }

        return _tokenProvider.
            Token(userSelected);
    }
    
}