using System.Threading;
using System.Threading.Tasks;
using JWT.Application.User.Command.ConfirmUserEmail;
using JWT.Application.User.Query.GenerateEmailConfirmation.Token;
using JWT.Application.User.Query.GetUserById;
using JWT.Domain.Entities;
using JWT.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace JWT.Application.User.Command.ForceEmailConfirmation
{
    public class ForceEmailConfirmationCommandHandler : IRequestHandler<ForceEmailConfirmationCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public ForceEmailConfirmationCommandHandler(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        public async Task<bool> Handle(ForceEmailConfirmationCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(request.UserId), cancellationToken);

            if (user == null)
            {
                throw new InvalidUserException();
            }

            if (await _userManager.IsEmailConfirmedAsync(user))
            {
                throw new EmailIsAlreadyConfirmedException();
            }

            var token = await _mediator.Send(new GenerateEmailConfirmationTokenQuery(user), cancellationToken);
            return await _mediator.Send(new ConfirmUserEmailCommand(userId: Base64UrlEncoder.Encode(request.UserId), token: Base64UrlEncoder.Encode(token)), cancellationToken);
        }
    }
}
