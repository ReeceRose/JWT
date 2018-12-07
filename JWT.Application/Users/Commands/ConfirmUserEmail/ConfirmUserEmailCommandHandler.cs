using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace JWT.Application.Users.Commands.ConfirmUserEmail
{
    public class ConfirmUserEmailCommandHandler : IRequestHandler<ConfirmUserEmailCommand, bool>
    {
        public Task<bool> Handle(ConfirmUserEmailCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
