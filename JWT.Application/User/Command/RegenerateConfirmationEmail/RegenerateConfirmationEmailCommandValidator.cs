using FluentValidation;

namespace JWT.Application.User.Command.RegenerateConfirmationEmail
{
    public class RegenerateConfirmationEmailCommandValidator : AbstractValidator<RegenerateConfirmationEmailCommand>
    {
        public RegenerateConfirmationEmailCommandValidator()
        {
            RuleFor(c => c.Email)
                .EmailAddress().WithMessage("Email is required")
                .NotEmpty().WithMessage("Email is required");
        }
    }
}
