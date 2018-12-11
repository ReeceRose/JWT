using System.Threading;
using System.Threading.Tasks;
using System.Web;
using JWT.Application.Users.Queries.GetUserById;
using JWT.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWT.Application.Users.Commands.ConfirmUserEmail
{
    public class ConfirmUserEmailCommandHandler : IRequestHandler<ConfirmUserEmailCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _userManager;

        public ConfirmUserEmailCommandHandler(IMediator mediator, UserManager<IdentityUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }
        public async Task<bool> Handle(ConfirmUserEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(request.UserId), cancellationToken);
            if (user == null)
            {
                throw new InvalidUserException();
            }

            var result = await _userManager.ConfirmEmailAsync(user, request.Token);

            return await Task.FromResult(result.Succeeded);
        }
    }
}
