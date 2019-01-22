using System.Threading;
using System.Threading.Tasks;
using JWT.Application.User.Query.GetUserById;
using JWT.Domain.Entities;
using JWT.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWT.Application.User.Command.RemoveUser
{
    public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public RemoveUserCommandHandler(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        public async Task<bool> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(request.UserId), cancellationToken);
            if (user == null)
            {
                throw new InvalidUserException();
            }

            var result = await _userManager.DeleteAsync(user);

            return await Task.FromResult(result.Succeeded);
        }
    }
}
