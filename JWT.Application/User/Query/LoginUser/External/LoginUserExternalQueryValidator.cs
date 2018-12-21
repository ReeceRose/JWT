using FluentValidation;

namespace JWT.Application.User.Query.LoginUser.External
{
    public class LoginUserExternalQueryValidator : AbstractValidator<LoginUserExternalQuery>
    {
        public LoginUserExternalQueryValidator()
        {
            RuleFor(u => u.AccessToken)
                .NotNull().WithMessage("Access token required");

        }
    }
}
