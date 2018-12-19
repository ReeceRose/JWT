using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace JWT.Application.User.Command.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        public Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}