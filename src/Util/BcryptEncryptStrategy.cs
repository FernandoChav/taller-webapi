namespace Taller1.Util;

public class BcryptEncryptStrategy : IEncryptStrategy 
{

    public string Encrypt(string password)
    {
        if (password.Length == 0)
        {
            throw new Exception("The password is empty");
        }
        
        return BCrypt.Net.BCrypt.HashPassword(password);
}

    public bool Verify(string passwordEntered, string passwordEncrypt)
    {
        if (passwordEncrypt.Length == 0 || passwordEntered.Length == 0)
        {
            throw new Exception("The password is empty");
        }
        
        return BCrypt.Net.BCrypt.Verify(passwordEntered, passwordEncrypt);
    }
    
}