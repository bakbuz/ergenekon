using Ergenekon.Application.Authentication.Commands.Register;
using Ergenekon.Application.Authentication.Services;
using Ergenekon.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Ergenekon.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthenticationService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> CreateAsync(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            UserName = request.Username,
            Email = request.Email,
            CreatedAt = DateTime.Now
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        //if(result.)
    }
}
