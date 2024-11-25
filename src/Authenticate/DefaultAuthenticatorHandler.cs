using System.Security.Authentication;
using Microsoft.EntityFrameworkCore;
using Taller1.Authenticate.Token;
using Taller1.Data;
using Taller1.Model;
using Taller1.Search;
using Taller1.Service;
using Taller1.TException;
using Taller1.Util;

namespace Taller1.Authenticate;

/// <summary>
/// Handles the authentication process for users. 
/// This class validates user credentials, checks the account status, 
/// and generates a JWT token if the authentication is successful.
/// </summary>
public class DefaultAuthenticatorHandler : IAuthenticatorHandler
{
    private readonly DbSet<User> _users;
    private readonly IEncryptStrategy _encryptStrategy;
    private readonly IUserTokenProvider _tokenProvider;
    
    /// <summary>
    /// Initializes the authentication handler with the necessary dependencies.
    /// </summary>
    /// <param name="applicationDbContext">The application database context.</param>
    /// <param name="encryptStrategy">The encryption strategy for password verification.</param>
    public DefaultAuthenticatorHandler(ApplicationDbContext applicationDbContext,
        IEncryptStrategy encryptStrategy,
        IUserTokenProvider tokenProvider)
    {
        _users = applicationDbContext.Users;
        _encryptStrategy = encryptStrategy;
        _tokenProvider = tokenProvider;
    }


    /// <summary>
    /// Authenticates a user based on the provided credentials.
    /// The method verifies the user's email, checks if the user is active, 
    /// and compares the password with the stored hashed password. 
    /// If successful, a JWT token is generated and returned.
    /// </summary>
    /// <param name="credentials">The user credentials containing the email and password.</param>
    /// <returns>A JWT token string if authentication is successful.</returns>
    /// <exception cref="AuthenticationUserException">Thrown if the user is not found or the user is inactive.</exception>
    /// <exception cref="AuthenticationException">Thrown if the password does not match.</exception>
    public string Authenticate(Credentials credentials)
    {
        var email = credentials.Email();
        var password = credentials.Password();

        var userSelected = DbSearchBuilder<User>
            .NewBuilder(_users)
            .Filter(user => user.Email == email)
            .BuildAndGetFirst();

        if (userSelected is null)
        {
            throw new AuthenticationUserException("Credentials incorrect");
        }

        if (!userSelected.IsActive)
        {
            throw new AuthenticationUserException("User is inactive");
        }

        if (!_encryptStrategy.Verify(password, userSelected.Password))
        {
            throw new AuthenticationException("Credentials incorrect");
        }

        return _tokenProvider.
            Token(userSelected);
    }
    
}