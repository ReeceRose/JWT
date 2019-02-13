using FluentValidation;

namespace JWT.Application.User.Command.RemoveUserClaim
{
    public class RemoveUserClaimValidator : AbstractValidator<RemoveUserClaimCommand>
    {
        public RemoveUserClaimValidator()
        {
            RuleFor(c => c.User)
                .NotNull().WithMessage("User required");

            RuleFor(c => c.Key)
                .NotEmpty().WithMessage("Key required")
                .NotNull().WithMessage("Key required");
        }
    }
}
