using Taller1.Model;
using Taller1.Util;

namespace Taller1.Mapper;

/// <summary>
/// Mapper class to transform a <see cref="User"/> object into a <see cref="UserView"/> object.
/// Implements the <see cref="IObjectMapper{TObject, TResult}"/> interface.
/// </summary>
public class ToUserViewMapper : IObjectMapper<User,  
UserView>
{
    /// <summary>
    /// Maps a <see cref="User"/> object to a <see cref="UserView"/> object.
    /// </summary>
    /// <param name="element">The <see cref="User"/> entity representing user details from the database.</param>
    /// <param name="parameters">
    /// Optional <see cref="ObjectParameters"/> for additional mapping context (not used in this implementation).
    /// </param>
    /// <returns>
    /// A <see cref="UserView"/> object containing a subset of user information meant for presentation or API responses.
    /// </returns>
    public UserView Mapper(User element, ObjectParameters? parameters)
    {
        return new UserView
        {
            Id = element.Id,
            Rut = element.Rut,
            Name = element.Name,
            Birthdate = element.Birthdate,
            GenderType = element.Gender,
            IsActive = element.IsActive
        };
    }
    
}
