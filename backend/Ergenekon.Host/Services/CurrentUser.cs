using Ergenekon.Application.Common.Interfaces;
using System.Security.Claims;

namespace Ergenekon.Host.Services;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? Id
    {
        get
        {
            var identifier = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(identifier) && Guid.TryParse(identifier, out var id))
            {
                return id;
            }
            return null;
        }
    }
}
