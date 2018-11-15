using FluentValidation;

namespace Core.Requests.Authentication.Login
{
    public class UserLoginRequestValidator : AbstractValidator<UserLoginRequest>
    {
        public UserLoginRequestValidator()
        {
            RuleFor(u => u.Email)
                .EmailAddress()
                .NotEmpty()
                .WithMessage("Not a valid email");

            RuleFor(u => u.Password)
                .NotEmpty()
                .WithMessage("Password is required");
        }
    }
}
