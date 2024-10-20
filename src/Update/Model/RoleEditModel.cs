using Taller1.Model;

namespace Taller1.Update.Model;

public class RoleEditModel : IUpdateModel<RoleEdit, Role>
{
    
    public void Edit(RoleEdit editObject, Role modelObject)
    {
        if (editObject.Name != null)
        {
            modelObject.Name = editObject.Name;
        }   
        
    }
    
}