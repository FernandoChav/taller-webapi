using Taller1.Model;

namespace Taller1.Authenticate.Token
{
    public interface IUserTokenProvider
    {

        string Token(User user);

    }

}
