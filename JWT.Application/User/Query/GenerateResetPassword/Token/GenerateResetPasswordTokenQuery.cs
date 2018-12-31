using JWT.Application.User.Model;
using MediatR;

namespace JWT.Application.User.Query.GenerateResetPassword.Token
{
    public class GenerateResetPasswordTokenQuery : IRequest<string>
    {
        public GenerateResetPasswordTokenQuery(ApplicationUserDto user) => User = user;

        public ApplicationUserDto User { get; }
    }
}
