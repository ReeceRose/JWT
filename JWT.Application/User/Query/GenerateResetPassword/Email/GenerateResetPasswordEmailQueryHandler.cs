using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace JWT.Application.User.Query.GenerateResetPassword.Email
{
    public class GenerateResetPasswordEmailQueryHandler : IRequestHandler<GenerateResetPasswordEmailQuery, bool>
    {
        public Task<bool> Handle(GenerateResetPasswordEmailQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
