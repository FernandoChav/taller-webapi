namespace Taller1.Model;

/// <summary>
/// This class is a simple token wrapper
/// </summary>

public class AuthenticationResponse
{

    /// <summary>
    /// This attribute is string that contains a token
    /// </summary>

    public Token Token { get; set; } = new Token();
    public Role Role { get; set; } = new Role();

    public UserView UserView { get; set; } = new UserView();

}

public class Token
{
    public string TokenContent { get; set; } = string.Empty;
}