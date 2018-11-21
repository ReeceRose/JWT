using FluentValidation;

namespace JWT.Application.Users.Queries.LoginUser
{
    public class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
    {
        public LoginUserQueryValidator()
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
