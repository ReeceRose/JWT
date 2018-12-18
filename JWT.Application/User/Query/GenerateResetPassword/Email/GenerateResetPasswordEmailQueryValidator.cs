using FluentValidation;

namespace JWT.Application.User.Query.GenerateResetPassword.Email
{
    public class GenerateResetPasswordEmailQueryValidator : AbstractValidator<GenerateResetPasswordEmailQuery>
    {
        public GenerateResetPasswordEmailQueryValidator()
        {
            RuleFor(c => c.Email)
                .NotNull().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is required");
        }
    }
}