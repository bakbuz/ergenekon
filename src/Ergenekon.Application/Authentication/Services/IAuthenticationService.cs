using Ergenekon.Application.Authentication.Commands.Register;
using Ergenekon.Application.Authentication.Shared;

namespace Ergenekon.Application.Authentication.Services;

public interface IAuthenticationService
{
    Task<IdentityResult> CreateAsync(RegisterCommand request, CancellationToken cancellationToken);
}
