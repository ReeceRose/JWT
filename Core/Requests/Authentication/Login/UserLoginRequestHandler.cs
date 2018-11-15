using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Requests.Authentication.Login
{
    public class UserLoginRequestHandler : IRequestHandler<UserLoginRequest, bool>
    {
        public Task<bool> Handle(UserLoginRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}
