using FluentValidation;

namespace JWT.Application.User.Query.GetAUserById
{
    public class GetAUserByIdQueryValidator : AbstractValidator<GetAUserByIdQuery>
    {
        public GetAUserByIdQueryValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("User ID required")
                .NotNull().WithMessage("User ID required");
        }
    }
}
