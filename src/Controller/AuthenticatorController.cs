using Microsoft.AspNetCore.Mvc;
using Taller1.Authenticate;
using Taller1.Model;

namespace Taller1.Controller;

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

    [HttpPost]
    [Route("/authenticate")]
    public ActionResult<Token> Authenticate([FromBody] AuthenticationCredential authenticationCredential)
    {
        var credentials = new Credentials(
            new Dictionary<string, string>
            {
                ["Email"] = authenticationCredential.Email,
                ["Password"] = authenticationCredential.Password
            }
        );

        var token = _authenticator.Authenticate(credentials);
        return new Token
        {
            TokenContent = token
        };
    }

    [HttpPost]
    [Route("/register")]
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