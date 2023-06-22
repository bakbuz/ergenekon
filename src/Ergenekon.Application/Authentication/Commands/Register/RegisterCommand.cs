using Ergenekon.Application.Authentication.Services;
using Ergenekon.Application.Common.Models;
using MediatR;

namespace Ergenekon.Application.Authentication.Commands.Register;

public sealed record RegisterCommand(string Username, string Email, string Password) : IRequest<(Result Result, string UserId)>
{

}
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, (Result Result, string UserId)>
{
    private readonly IAuthenticationService _authenticationService;

    public RegisterCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<(Result Result, string UserId)> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await _authenticationService.CreateAsync(request.Username, request.Email, request.Password);
    }
}