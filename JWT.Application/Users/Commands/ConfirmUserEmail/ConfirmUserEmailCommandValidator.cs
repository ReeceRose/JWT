using FluentValidation;

namespace JWT.Application.Users.Commands.ConfirmUserEmail
{
    public class ConfirmUserEmailCommandValidator : AbstractValidator<ConfirmUserEmailCommand>
    {
        public ConfirmUserEmailCommandValidator()
        {
            RuleFor(c => c.Code)
                .NotEmpty().WithMessage("Code required");
        }
    }
}
