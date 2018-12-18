using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace JWT.Application.User.Query.GenerateResetPassword.Token
{
    public class GenerateResetPasswordTokenQueryHandler : IRequestHandler<GenerateResetPasswordTokenQuery, string>
    {
        public Task<string> Handle(GenerateResetPasswordTokenQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
