using FluentValidation;

namespace JWT.Application.User.Command.RegenerateConfirmationEmail
{
    public class RegenerateConfirmationEmailCommandValidator : AbstractValidator<RegenerateConfirmationEmailCommand>
    {
        public RegenerateConfirmationEmailCommandValidator()
        {
            RuleFor(c => c.Email)
                .NotNull().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is required");
        }
    }
}
