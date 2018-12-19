using FluentValidation;

namespace JWT.Application.User.Command.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(r => r.Token)
                .NotNull().WithMessage("Token required")
                .NotEmpty().WithMessage("Token required");

            RuleFor(r => r.Email)
                .NotNull().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is required");

            RuleFor(r => r.Password)
                .NotNull().WithMessage("Password does not meet security constraints")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$")
                .WithMessage("Password does not meet security constraints");
        }
    }
}