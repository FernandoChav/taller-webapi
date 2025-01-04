using Taller1.Model;
using Taller1.Util;

namespace Taller1.Update.Model;

/// <summary>
/// Represents the logic for updating a User object.
/// </summary>
public class UserEditModel : IUpdateModel<User>
{
    private readonly IEncryptStrategy _encryptStrategy;

    /// <summary>
    /// Initializes a new instance of the UserEditModel class with the specified encrypt strategy.
    /// </summary>
    /// <param name="encryptStrategy">The encryption strategy used for encrypting user passwords.</param>
    public UserEditModel(IEncryptStrategy encryptStrategy)
    {
        _encryptStrategy = encryptStrategy;
    }

    /// <summary>
    /// Edits the properties of a User object based on the provided parameters.
    /// </summary>
    /// <param name="parameters">The parameters containing the values to update in the User object.</param>
    /// <param name="modelObject">The User object to be updated.</param>
    public void Edit(ObjectParameters parameters, User modelObject)
    {
        parameters.ExecuteIfExists("Name", obj => { modelObject.Name = (string)obj; });

        parameters.ExecuteIfExists("Gender", obj =>
        {
            var str = obj as string;
            if (Enum.TryParse(str, out GenderType genderType))
            {
                modelObject.Gender = genderType;
            }
        });

        parameters.ExecuteIfExists("IsActive", obj =>
        {
            var str = obj as string;
            var isActive = bool.Parse(str);
            modelObject.IsActive = isActive;
        });

        parameters.ExecuteIfExists("Password", obj =>
        {
            var str = (string)obj;
            var newPasswordEncrypt = _encryptStrategy.Encrypt(str);
            modelObject.Password = newPasswordEncrypt;
            
        });
        
    }
}