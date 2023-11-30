using FluentValidation;

namespace Ergenekon.Infrastructure.Identity.Models;

public record ConfirmEmailChangeRequest(string UserId, string Email, string Code);

public class ConfirmEmailChangeRequestValidator : AbstractValidator<ConfirmEmailChangeRequest>
{
    public ConfirmEmailChangeRequestValidator()
    {
        RuleFor(m => m.UserId).NotEmpty();
        RuleFor(m => m.Code).NotEmpty();
        RuleFor(m => m.Email).NotEmpty().WithName("E-posta").EmailAddress();
    }
}
