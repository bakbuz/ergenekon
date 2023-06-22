using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Application.Common.Models;
using MediatR;

namespace Ergenekon.Application.Authentication.Commands.Login;

public sealed record LoginCommand(string Username, string Email, string Password) : IRequest<(Result Result, string UserId)>
{

}
public class LoginCommandHandler : IRequestHandler<LoginCommand, (Result Result, string UserId)>
{
    private readonly IIdentityService _identityService;

    public LoginCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<(Result Result, string UserId)> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.CreateUserAsync(request.Username, request.Email, request.Password);
    }
}