using FluentValidation;

namespace Core.Requests.Authentication.Register
{
    public class UserRegisterRequestValidator : AbstractValidator<UserRegisterRequest>
    {
        public UserRegisterRequestValidator()
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
