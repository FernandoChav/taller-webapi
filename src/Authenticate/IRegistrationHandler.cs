using Taller1.Model;

namespace Taller1.Authenticate;

/// <summary>
/// This interface manage the way for create a new user, registration system
/// </summary>

public interface IRegistrationHandler
{

    /// <summary>
    /// Create a new user from a request user creation
    /// </summary>
    /// <param name="userCreation">A set parameters that contains a data for create a new user</param>
    /// <returns>A response that contains if the user were created succesful</returns>

    RegistrationResponse Register(UserCreation userCreation);

}