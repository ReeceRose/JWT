using FluentValidation;

namespace JWT.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(u => u.Email)
                .EmailAddress()
                .NotEmpty()
                .WithMessage("Not a valid email");

            RuleFor(u => u.Password)
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$")
                .NotEmpty()
                .WithMessage("Password is required");
        }
    }
}
