using Taller1.Model;
using Taller1.Util;

namespace Taller1.Update.Model;

public class UserEditModel : IUpdateModel<UserEditGeneral, User>
{
    private readonly IEncryptStrategy _encryptStrategy;

    public UserEditModel(IEncryptStrategy encryptStrategy)
    {
        _encryptStrategy = encryptStrategy;
    }

    public void Edit(UserEditGeneral editObject, User modelObject)
    {
        if (editObject.Name != null)
        {
            modelObject.Name = editObject.Name;
        }

        if (editObject.Gender != null)
        {
            modelObject.Gender = editObject.Gender.Value;
        }

        if (editObject.IsActive != null)
        {
            modelObject.IsActive = editObject.IsActive.Value;
        }

        if (editObject.Password != null &&
            (editObject.Password != editObject.RepeatPassword))
        {
            var newPasswordEncrypt = _encryptStrategy.Encrypt(editObject.Password);
            modelObject.Password = newPasswordEncrypt;
        }
    }
}