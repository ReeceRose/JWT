using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace JWT.Application.Users.Queries.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, bool>
    {
        public Task<bool> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}
