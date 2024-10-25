using Taller1.Model;
using Taller1.Util;

namespace Taller1.Update.Model;

public class UserEditModel : IUpdateModel<User>
{
    private readonly IEncryptStrategy _encryptStrategy;

    public UserEditModel(IEncryptStrategy encryptStrategy)
    {
        _encryptStrategy = encryptStrategy;
    }

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
            var isActive = (bool)obj;
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