using FluentValidation;

namespace JWT.Application.User.Command.ForceEmailConfirmation
{
    public class ForceEmailConfirmationCommandValidator : AbstractValidator<ForceEmailConfirmationCommand>
    {
        public ForceEmailConfirmationCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("User ID required")
                .NotNull().WithMessage("User ID required");
        }
    }
}
