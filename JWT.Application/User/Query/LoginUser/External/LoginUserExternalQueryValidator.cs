using FluentValidation;

namespace JWT.Application.User.Query.LoginUser.External
{
    public class LoginUserExternalQueryValidator : AbstractValidator<LoginUserExternalQuery>
    {
        public LoginUserExternalQueryValidator()
        {
            RuleFor(u => u.AccessToken)
                .NotEmpty().WithMessage("Access token required")
                .NotNull().WithMessage("Access token required");

        }
    }
}
