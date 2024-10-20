using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Taller1.Model;
using Taller1.Service;
namespace Taller1.Authenticate.Token
{
    public class JwtUserTokenProvider : IUserTokenProvider
    {
        private readonly string _jwtSecret;
        private readonly string _validIssuer;
        private readonly string _validAudience;
        private readonly IObjectRepository<Role, RoleEdit> _roleRepository;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public JwtUserTokenProvider(IConfiguration configuration,
            IObjectRepository<Role, RoleEdit> roleRepository)
        {
            _jwtSecret = configuration["JWT:Secret"];
            _validIssuer = configuration["JWT:ValidIssuer"];
            _validAudience = configuration["JWT:ValidAudience"];
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            _roleRepository = roleRepository;
        }

        public string Token(User user)
        {
            var role = _roleRepository.FindById(user.RoleId);
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, role.Name)
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            DateTime? expiration = DateTime.UtcNow.AddHours(1);
            var credentials = new SigningCredentials(
                authSigningKey,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return _jwtSecurityTokenHandler
                .WriteToken(token);
        }
    }
}