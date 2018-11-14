using FluentValidation;

namespace Data.Models.v1.Authentication.Login
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
