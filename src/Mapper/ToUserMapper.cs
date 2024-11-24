using Taller1.Model;
using Taller1.Util;

namespace Taller1.Mapper;

/// <summary>
/// Mapper class to convert a <see cref="UserCreation"/> object into a <see cref="User"/> object.
/// Implements the <see cref="IObjectMapper{TObject, TResult}"/> interface.
/// </summary>
public class ToUserMapper : IObjectMapper<UserCreation,
User>
{

    private readonly User _userEmpty = new User { };
    
    /// <summary>
    /// Maps a <see cref="UserCreation"/> object to a <see cref="User"/> object.
    /// </summary>
    /// <param name="element">The <see cref="UserCreation"/> object containing user input data.</param>
    /// <param name="parameters">
    /// Optional <see cref="ObjectParameters"/> containing additional information for mapping, such as the user's encrypted password.
    /// </param>
    /// <returns>
    /// A <see cref="User"/> object populated with the mapped data from the input <see cref="UserCreation"/> and parameters.
    /// </returns>
    public User Mapper(UserCreation element, ObjectParameters? parameters)
    {
        if (parameters == null)
        {
            return _userEmpty;
        }
        
        var password = parameters.GetString("password");
        
        return new User
        {
            Name = element.Name,
            Rut = element.Rut,
            Birthdate = element.Birthdate,
            Password = password,
            Email = element.Email,
            Gender = element.GenderType,
            IsActive = true,
            RoleId = 1
        };
        
    }
    
}