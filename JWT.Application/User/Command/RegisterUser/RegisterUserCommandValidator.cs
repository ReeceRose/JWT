using FluentValidation;

namespace JWT.Application.User.Command.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(u => u.Email)
                .EmailAddress()
                    .WithMessage("Not a valid email");

            RuleFor(u => u.Password)
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$")
                    .WithMessage("Password does not meet security constraints");
        }
    }
}
