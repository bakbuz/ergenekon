using Ergenekon.Domain.Consts;
using FluentValidation;

namespace Ergenekon.Application.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(m => m.Username).NotEmpty();
        RuleFor(m => m.Email).NotEmpty().EmailAddress();
        RuleFor(m => m.Password).NotEmpty().MinimumLength(IdentityConsts.PasswordMinimumLength);
    }
}
