using Taller1.Model;
using Taller1.Util;

namespace Taller1.Update.Model;

/// <summary>
/// Represents the logic for updating a Role object.
/// </summary>
public class RoleEditModel : IUpdateModel<Role>
{
    /// <summary>
    /// Edits the properties of a Role object based on the provided parameters.
    /// </summary>
    /// <param name="parameters">The parameters containing the values to update in the Role object.</param>
    /// <param name="modelObject">The Role object to be updated.</param>
    public void Edit(ObjectParameters parameters, Role modelObject)
    {
        
        parameters.ExecuteIfExists("Name", obj =>
        {
            modelObject.Name = (string)obj;
        });
        
    }
    
}