using FluentValidation;

namespace JWT.Application.User.Query.GenerateEmailConfirmation.Email
{
    public class GenerateEmailConfirmationEmailQueryValidator : AbstractValidator<GenerateEmailConfirmationEmailQuery>
    {
        public GenerateEmailConfirmationEmailQueryValidator()
        {
            RuleFor(c => c.Email)
                .NotNull().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is required");
        }
    }
}
