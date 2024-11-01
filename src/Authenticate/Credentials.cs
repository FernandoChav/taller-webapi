namespace Taller1.Authenticate;

/// <summary>
/// This class contains a set from credentials for access to any system 
/// </summary>

public class Credentials
{
    
    /// <summary>
    /// Dictionary that contains data credentials 
    /// </summary>
    
    private readonly Dictionary<string, string> _data;

    public Credentials(Dictionary<string, string> data)
    {
        _data = data;
    }

    /// <summary>
    /// Get user credential
    /// </summary>
    /// <returns>A user credential</returns>
    
    public string User()
    {
        return Get("User");
    }

    /// <summary>
    /// Get password credential
    /// </summary>
    /// <returns></returns>
    
    public string Password()
    {
        return Get("Password");
    }
    
    /// <summary>
    /// Get email credential
    /// </summary>
    /// <returns></returns>
    
    public string Email()
    {
        return Get("Email");
    }
    
    /// <summary>
    /// Get element from the credentials
    /// </summary>
    /// <param name="key">Key credential</param>
    /// <returns>A element from credentials</returns>
    
    public string Get(string key)
    {
        return _data[key];
    }
    
}