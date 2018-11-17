using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace JWT.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        public Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}