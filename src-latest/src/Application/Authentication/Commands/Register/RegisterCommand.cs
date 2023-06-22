using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Application.Common.Models;
using MediatR;

namespace Ergenekon.Application.Authentication.Commands.Register;

public sealed record RegisterCommand(string Username, string Email, string Password) : IRequest<(Result Result, string UserId)>
{

}
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, (Result Result, string UserId)>
{
    private readonly IIdentityService _identityService;

    public RegisterCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<(Result Result, string UserId)> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.CreateUserAsync(request.Username, request.Email, request.Password);
    }
}