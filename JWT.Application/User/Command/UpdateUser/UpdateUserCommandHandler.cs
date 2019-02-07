using System.Threading;
using System.Threading.Tasks;
using JWT.Domain.Exceptions;
using JWT.Persistence;
using MediatR;

namespace JWT.Application.User.Command.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public UpdateUserCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.User == null) { throw new InvalidUserException(); }
            _context.Users.Update(request.User);
            await _context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(true);
        }
    }
}
