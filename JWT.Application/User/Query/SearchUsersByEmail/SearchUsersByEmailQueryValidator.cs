using FluentValidation;

namespace JWT.Application.User.Query.SearchUsersByEmail
{
    public class SearchUsersByEmailQueryValidator : AbstractValidator<SearchUsersByEmailQuery>
    {
        public SearchUsersByEmailQueryValidator()
        {
            RuleFor(u => u.Email)
                .NotNull().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is required");
        }
    }
}
