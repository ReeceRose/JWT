using System.Threading;
using System.Threading.Tasks;
using JWT.Application.User.Command.UpdateUser;
using JWT.Application.User.Query.GetUserById;
using JWT.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JWT.Application.User.Command.EnableUser
{
    public class EnableUserCommandHandler: IRequestHandler<EnableUserCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EnableUserCommandHandler> _logger;

        public EnableUserCommandHandler(IMediator mediator, ILogger<EnableUserCommandHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<bool> Handle(EnableUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(request.UserId), cancellationToken);
            // Should not be the case as the ID is not being touched by the user at all
            if (user == null)
            {
                throw new InvalidUserException();
            }

            user.AccountEnabled = true;
            _logger.LogInformation($"Enable User: {request.UserId}: Account enabled");
            await _mediator.Send(new UpdateUserCommand(user), cancellationToken);
            return await Task.FromResult(true);
        }
    }
}
