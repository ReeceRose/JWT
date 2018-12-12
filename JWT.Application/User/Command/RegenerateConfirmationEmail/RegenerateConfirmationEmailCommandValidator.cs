using FluentValidation;

namespace JWT.Application.User.Command.RegenerateConfirmationEmail
{
    public class RegenerateConfirmationEmailCommandValidator : AbstractValidator<RegenerateConfirmationEmailCommand>
    {
        public RegenerateConfirmationEmailCommandValidator()
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email is required");
        }
    }
}
