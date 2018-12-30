using FluentValidation;

namespace JWT.Application.User.Query.GetUserClaim
{
    public class GetUserClaimQueryValidator : AbstractValidator<GetUserClaimQuery>
    {
        public GetUserClaimQueryValidator()
        {
            RuleFor(q => q.User)
                .NotEmpty().WithMessage("User required")
                .NotNull().WithMessage("User required");
        }
    }
}
