using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Taller1.Model;
using Taller1.Service;
namespace Taller1.Authenticate.Token
{
     /// <summary>
    /// Provides JWT tokens for users. 
    /// This class is responsible for generating a signed JWT token for a user, 
    /// which contains the user's name, email, and role.
    /// </summary>
    public class JwtUserTokenProvider : IUserTokenProvider
    {
        private readonly string _jwtSecret;
        private readonly string _validIssuer;
        private readonly string _validAudience;
        private readonly IObjectRepository<Role> _roleRepository;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        /// <summary>
        /// Initializes the JWT token provider with the required configuration.
        /// </summary>
        /// <param name="configuration">The application configuration containing the JWT keys.</param>
        /// <param name="roleRepository">The role repository to retrieve the user's role information.</param>
        public JwtUserTokenProvider(IConfiguration configuration,
            IObjectRepository<Role> roleRepository)
        {
            _jwtSecret = configuration["JWT:Secret"];
            _validIssuer = configuration["JWT:ValidIssuer"];
            _validAudience = configuration["JWT:ValidAudience"];
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// Generates a JWT token for the specified user.
        /// The token includes the user's name, email, and role.
        /// </summary>
        /// <param name="user">The user for whom the token is generated.</param>
        public string Token(User user)
        {

            var roleId = user.RoleId;
            Console.WriteLine("Role Id = " + roleId);
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