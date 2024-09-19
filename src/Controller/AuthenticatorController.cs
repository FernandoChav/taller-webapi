using Microsoft.AspNetCore.Mvc;
using Taller1.Authenticate;
using Taller1.Model;

namespace Taller1.Controller;

[ApiController]
[Route("api/[controller]")]
public class AuthenticatorController : ControllerBase
{

    private IAuthenticatorHandler _authenticator;

    public AuthenticatorController(IAuthenticatorHandler authenticator)
    {
        _authenticator = authenticator;
    }

    [HttpPost]
    [Route("/authenticate")]
    public ActionResult<Token> Authenticate(AuthenticationCredential authenticationCredential)
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

}