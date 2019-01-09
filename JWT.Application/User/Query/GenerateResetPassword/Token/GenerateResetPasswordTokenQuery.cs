using JWT.Application.User.Model;
using JWT.Domain.Entities;
using MediatR;

namespace JWT.Application.User.Query.GenerateResetPassword.Token
{
    public class GenerateResetPasswordTokenQuery : IRequest<string>
    {
        public GenerateResetPasswordTokenQuery(ApplicationUser user) => User = user;

        public ApplicationUser User { get; }
    }
}
