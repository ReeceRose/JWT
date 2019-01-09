using JWT.Application.User.Model;
using JWT.Domain.Entities;
using MediatR;

namespace JWT.Application.User.Query.GenerateEmailConfirmation.Token
{
    public class GenerateEmailConfirmationTokenQuery : IRequest<string>
    {
        public GenerateEmailConfirmationTokenQuery(ApplicationUser user) => User = user;

        public ApplicationUser User { get; }
    }
}