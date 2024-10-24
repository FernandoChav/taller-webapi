using Taller1.Model;
using Taller1.Util;

namespace Taller1.Mapper;

public class ToUserView : IObjectMapper<User,  
UserView>
{
    public UserView Mapper(User element, ObjectParameters? parameters)
    {
        return new UserView
        {
            Rut = element.Rut,
            Name = element.Name,
            Birthdate = element.Birthdate,
            GenderType = element.Gender,
            IsActive = element.IsActive
        };
    }
}