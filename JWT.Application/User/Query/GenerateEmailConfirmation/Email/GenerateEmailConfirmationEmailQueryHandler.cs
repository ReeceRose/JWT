using System.Threading;
using System.Threading.Tasks;
using JWT.Application.Interfaces;
using JWT.Application.User.Query.GenerateEmailConfirmation.Token;
using JWT.Application.User.Query.GetUserByEmail;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace JWT.Application.User.Query.GenerateEmailConfirmation.Email
{
    public class GenerateEmailConfirmationEmailQueryHandler : IRequestHandler<GenerateEmailConfirmationEmailQuery, string>
    {
        private readonly IMediator _mediator;
        private readonly INotificationService _notificationService;
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;

        public GenerateEmailConfirmationEmailQueryHandler(IMediator mediator, INotificationService notificationService, IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            _mediator = mediator;
            _notificationService = notificationService;
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> Handle(GenerateEmailConfirmationEmailQuery request, CancellationToken cancellationToken)
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

            var token = _mediator.Send(new GenerateEmailConfirmationTokenQuery(user), cancellationToken).Result;

            await _notificationService.SendNotificationAsync(toName: request.Email, toEmailAddress: request.Email, subject: "Confirm your account",
                message: $"In order to login you must confirm your account. To continue click <a href='{_configuration["FrontEndUrl"]}/ConfirmEmail?userId={user.Id}&token={token}'>here</a>");
            
            return await Task.FromResult(token);
        }
    }
}