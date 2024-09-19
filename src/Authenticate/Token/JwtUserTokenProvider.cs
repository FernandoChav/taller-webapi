using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Taller1.Authenticate.Token;
using Taller1.Model;

namespace Taller1.src.Authenticate.Token
{
    public class JwtUserTokenProvider : IUserTokenProvider
    {
        private readonly string _jwtSecret;
        private readonly string _validIssuer;
        private readonly string _validAudience;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public JwtUserTokenProvider(IConfiguration configuration)
        {
            _jwtSecret = configuration["JWT:Secret"];
            _validIssuer = configuration["JWT:ValidIssuer"];
            _validAudience = configuration["JWT:ValidAudience"];
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        public string Token(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid()
                        .ToString())
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            DateTime? expiration = DateTime.UtcNow.AddHours(3);
            var credentials = new SigningCredentials(
                authSigningKey,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                _validIssuer,
                _validAudience,
                claims,
                null,
                expiration,
                credentials
            );

            return _jwtSecurityTokenHandler
                .WriteToken(token);
        }
    }
}