using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace JWT.Application.Requests.Authentication.Register
{
    public class UserRegisterRequestHandler : IRequestHandler<UserRegisterRequest, bool>
    {
        public Task<bool> Handle(UserRegisterRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}