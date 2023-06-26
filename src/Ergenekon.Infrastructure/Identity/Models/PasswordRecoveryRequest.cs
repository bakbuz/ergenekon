using FluentValidation;

namespace Ergenekon.Infrastructure.Identity.Models;

public sealed record PasswordRecoveryRequest(string Email);

public class PasswordRecoveryValidator : AbstractValidator<PasswordRecoveryRequest>
{
    public PasswordRecoveryValidator()
    {
        RuleFor(m => m.Email).NotEmpty().EmailAddress();
    }
}
