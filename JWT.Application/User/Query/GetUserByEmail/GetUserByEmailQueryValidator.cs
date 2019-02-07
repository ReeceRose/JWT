using FluentValidation;

namespace JWT.Application.User.Query.GetUserByEmail
{
    public class GetUserByEmailQueryValidator : AbstractValidator<GetUserByEmailQuery>
    {
        public GetUserByEmailQueryValidator()
        {
            RuleFor(u => u.Email)
                .NotNull().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is required");
        }
    }
}
