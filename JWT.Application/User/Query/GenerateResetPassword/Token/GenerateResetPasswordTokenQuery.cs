using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWT.Application.User.Query.GenerateResetPassword.Token
{
    public class GenerateResetPasswordTokenQuery : IRequest<string>
    {
        public GenerateResetPasswordTokenQuery(IdentityUser user) => User = user;

        public IdentityUser User { get; }
    }
}
