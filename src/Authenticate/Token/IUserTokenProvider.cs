using Taller1.Model;

namespace Taller1.Authenticate.Token
{

    /// <summary>
    /// This interface is a provider that generate a token from a entity 
    /// model user
    /// </summary>

    public interface IUserTokenProvider
    {

        /// <summary>
        /// Generate a access token from a user 
        /// </summary>
        /// <param name="user">a entity model user</param>
        /// <returns>a string that represent a access token</returns>

        string Token(User user);

    }

}
