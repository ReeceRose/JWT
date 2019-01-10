using System;
using System.Threading;
using System.Threading.Tasks;
using JWT.Application.User.Query.GetUserById;
using JWT.Domain.Entities;
using JWT.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace JWT.Application.User.Command.ConfirmUserEmail
{
    public class ConfirmUserEmailCommandHandler : IRequestHandler<ConfirmUserEmailCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ConfirmUserEmailCommandHandler> _logger;

        public ConfirmUserEmailCommandHandler(IMediator mediator, UserManager<ApplicationUser> userManager, ILogger<ConfirmUserEmailCommandHandler> logger)
        {
            _mediator = mediator;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<bool> Handle(ConfirmUserEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(Base64UrlEncoder.Decode(request.UserId)), cancellationToken);

            if (user == null)
            {
                _logger.LogInformation($"Confirm User Email: {request.UserId}: Failed confirmation: User does not exist");
                throw new InvalidUserException();
            }
            
            var result = await _userManager.ConfirmEmailAsync(user,
                Base64UrlEncoder.Decode(request.Token));

            if (!result.Succeeded)
            {
                _logger.LogInformation($"Confirm User Email: {request.UserId}: Failed confirmation: {result.Errors}");
                throw new Exception("Failed to confirm email");
            }

            return await Task.FromResult(result.Succeeded);
        }
    }
}
