using System.Threading;
using System.Threading.Tasks;
using JWT.Application.ConfirmationEmail.Command;
using JWT.Application.User.Query.GetUserByEmail;
using JWT.Infrastructure.Notifications;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace JWT.Application.User.Command.RegenerateConfirmationEmail
{
    public class RegenerateConfirmationEmailCommandHandler : IRequestHandler<RegenerateConfirmationEmailCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly INotificationService _notificationService;
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;

        public RegenerateConfirmationEmailCommandHandler(IMediator mediator, INotificationService notificationService, IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            _mediator = mediator;
            _notificationService = notificationService;
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> Handle(RegenerateConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            var user = _mediator.Send(new GetUserByEmailQuery(request.Email), cancellationToken).Result;

            if (user == null)
            {
                return await Task.FromResult<string>(null);
            }

            if (await _userManager.IsEmailConfirmedAsync(user))
            {
                return await Task.FromResult<string>(null);
            }

            var token = _mediator.Send(new GenerateConfirmationTokenCommand(user), cancellationToken).Result;

            await _notificationService.SendNotificationAsync(toName: request.Email, toEmailAddress: request.Email, subject: "Confirm your account",
                message: $"You have requested a confirmation email be sent to you again. To continue click <a href='{_configuration["FrontEndUrl"]}/ConfirmEmail?userId={user.Id}&token={token}'>here</a>");
            
            return await Task.FromResult(token);
        }
    }
}