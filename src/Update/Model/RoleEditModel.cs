using Taller1.Model;
using Taller1.Util;

namespace Taller1.Update.Model;

public class RoleEditModel : IUpdateModel<Role>
{
    
    public void Edit(ObjectParameters parameters, Role modelObject)
    {
        
        parameters.ExecuteIfExists("Name", obj =>
        {
            modelObject.Name = (string)obj;
        });
        
    }
    
}