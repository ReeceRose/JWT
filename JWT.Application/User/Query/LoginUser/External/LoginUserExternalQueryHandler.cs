using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace JWT.Application.User.Query.LoginUser.External
{
    public class LoginUserExternalQueryHandler : IRequestHandler<LoginUserExternalQuery, string>
    {
        public Task<string> Handle(LoginUserExternalQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
