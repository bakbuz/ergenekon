namespace Ergenekon.Infrastructure.Identity.Models;

public class TokenValues
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public DateTime Expires { get; set; }
}
