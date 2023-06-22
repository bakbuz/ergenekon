namespace Ergenekon.Application.Authentication.Shared;

public class TokenValues
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public DateTime Expires { get; set; }
}
