using Taller1.Model;
using Taller1.Util;

namespace Taller1.Mapper;

public class ToUserMapper : IObjectMapper<UserCreation,
User>
{

    private readonly User _userEmpty = new User { };
    
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
            IsActive = true
        };
        
    }
    
}