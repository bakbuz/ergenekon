using Ergenekon.Domain.Consts;
using FluentValidation;

namespace Ergenekon.Application.Authentication.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(m => m.Email).NotEmpty().EmailAddress();
        RuleFor(m => m.Password).NotEmpty().MinimumLength(IdentityConsts.PasswordMinimumLength);
    }
}
