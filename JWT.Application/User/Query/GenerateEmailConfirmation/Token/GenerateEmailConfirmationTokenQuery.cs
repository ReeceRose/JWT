using JWT.Application.User.Model;
using MediatR;

namespace JWT.Application.User.Query.GenerateEmailConfirmation.Token
{
    public class GenerateEmailConfirmationTokenQuery : IRequest<string>
    {
        public GenerateEmailConfirmationTokenQuery(ApplicationUserDto user) => User = user;

        public ApplicationUserDto User { get; }
    }
}