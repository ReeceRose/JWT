using FluentValidation;

namespace JWT.Application.Users.Commands.RegenerateConfirmationEmail
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
