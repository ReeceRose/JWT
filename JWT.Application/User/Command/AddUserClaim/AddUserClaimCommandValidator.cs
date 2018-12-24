using FluentValidation;

namespace JWT.Application.User.Command.AddUserClaim
{
    public class AddUserClaimCommandValidator : AbstractValidator<AddUserClaimCommand>
    {
        public AddUserClaimCommandValidator()
        {
            RuleFor(c => c.User)
                .NotNull().WithMessage("User required");

            RuleFor(c => c.Key)
                .NotEmpty().WithMessage("Key required")
                .NotNull().WithMessage("Key required");

            RuleFor(c => c.Value)
                .NotEmpty().WithMessage("Value required")
                .NotEmpty().WithMessage("Value required");
        }
    }
}
