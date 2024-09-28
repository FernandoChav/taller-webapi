namespace Taller1.Util;

public interface IEncryptService
{

    string Encrypt(string password);

    bool Verify(string passwordEntered,
        string passwordEncrypt);

}