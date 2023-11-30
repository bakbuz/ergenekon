using FluentValidation;

namespace Ergenekon.Infrastructure.Identity.Models;

public record ConfirmEmailRequest(string UserId, string Code);

public class ConfirmEmailRequestValidator : AbstractValidator<ConfirmEmailRequest>
{
    public ConfirmEmailRequestValidator()
    {
        RuleFor(m => m.UserId).NotEmpty();
        RuleFor(m => m.Code).NotEmpty();
    }
}
