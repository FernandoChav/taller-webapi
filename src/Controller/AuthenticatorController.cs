﻿using Microsoft.AspNetCore.Mvc;
using Taller1.Authenticate;
using Taller1.Model;
using Taller1.TException;
using Taller1.Util;

namespace Taller1.Controller;

/// <summary>
/// This controller manage all endpoints associated with authenticate user 
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthenticatorController : ControllerBase
{
    private IAuthenticatorHandler _authenticator;
    private IRegistrationHandler _registrationHandler;

    public AuthenticatorController(IAuthenticatorHandler authenticator,
        IRegistrationHandler registrationHandler)
    {
        _authenticator = authenticator;
        _registrationHandler = registrationHandler;
    }

    /// <summary>
    /// Generate a authentication from user, this method retrieve a token that contains session user
    /// </summary>
    /// <param name="authenticationCredential">A set parameters for make the authenticaton</param>
    /// <returns>A token wrapped with autentication</returns>
    [HttpPost]
    [Route("/api/authenticate")]
    public ActionResult<Token> Authenticate([FromBody] AuthenticationCredential authenticationCredential)
    {
        var credentials = new Credentials(
            new Dictionary<string, string>
            {
                ["Email"] = authenticationCredential.Email,
                ["Password"] = authenticationCredential.Password
            }
        );
        
        var token = "";
        try

        {
            token = _authenticator.Authenticate(credentials);
        }
        catch (AuthenticationUserException exception)
        {
            return BadRequest(exception.Message);
        }

        return new Token
        {
            TokenContent = token
        };
    }

    /// <summary>
    /// Register a new user based a set properties 
    /// </summary>
    /// <param name="userCreation">a set properties from a user</param>
    /// <returns>A wrapper string with message response</returns>
    [HttpPost]
    [Route("/api/register")]
    public ActionResult<string> Register([FromBody] UserCreation userCreation)
    {
        var response = _registrationHandler.Register(userCreation);
        var message = response.GetMessage();

        if (!response.IsSuccessful())
        {
            return BadRequest(message);
        }

        return Ok(message);
    }
}