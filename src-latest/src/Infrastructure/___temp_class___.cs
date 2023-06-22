using Ergenekon.Application.Authentication.Commands.Register;
using Ergenekon.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Ergenekon.Infrastructure;

public interface IAuthenticationService
{
    void Create(RegisterCommand request, CancellationToken cancellationToken);
}

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthenticationService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public void Create(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            UserName = request.Username,
            Email = request.Email,
            CreatedAt = DateTime.Now
        };

        var result = _userManager.CreateAsync(user, request.Password);
        //if(result.)
    }
}
