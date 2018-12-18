using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWT.Application.User.Query.GenerateEmailConfirmation.Token
{
    public class GenerateEmailConfirmationTokenQuery : IRequest<string>
    {
        public GenerateEmailConfirmationTokenQuery(IdentityUser user) => User = user;

        public IdentityUser User { get; }
    }
}