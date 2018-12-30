using FluentValidation;

namespace JWT.Application.User.Command.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(c => c.User)
                .NotNull().WithMessage("User required");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Password required")
                .NotNull().WithMessage("Password required");
        }
    }
}
