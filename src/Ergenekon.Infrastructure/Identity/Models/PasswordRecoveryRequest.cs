using FluentValidation;

namespace Ergenekon.Infrastructure.Identity.Models;

public sealed record PasswordRecoveryRequest(string Email);

public class PasswordRecoveryRequestValidator : AbstractValidator<PasswordRecoveryRequest>
{
    public PasswordRecoveryRequestValidator()
    {
        RuleFor(m => m.Email).NotEmpty().WithName("E-posta").EmailAddress();
    }
}
