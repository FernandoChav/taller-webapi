namespace Taller1.Authenticate;

public class Credentials
{
    private readonly Dictionary<string, string> _data;

    public Credentials(Dictionary<string, string> data)
    {
        _data = data;
    }

    public string User()
    {
        return Get("User");
    }

    public string Password()
    {
        return Get("Password");
    }

    public string Email()
    {
        return Get("Email");
    }

    public string Get(string key)
    {
        return _data[key];
    }
    
}