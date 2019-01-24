using FluentValidation;

namespace JWT.Application.User.Command.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(c => c.User)
                .NotNull().WithMessage("User required");
        }
    }
}
