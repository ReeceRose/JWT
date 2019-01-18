using System.Threading;
using System.Threading.Tasks;
using JWT.Application.Interfaces;
using JWT.Application.User.Query.GenerateEmailConfirmation.Token;
using JWT.Application.User.Query.GetUserByEmail;
using JWT.Domain.Entities;
using JWT.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace JWT.Application.User.Query.GenerateEmailConfirmation.Email
{
    public class GenerateEmailConfirmationEmailQueryHandler : IRequestHandler<GenerateEmailConfirmationEmailQuery, string>
    {
        private readonly IMediator _mediator;
        private readonly INotificationService _notificationService;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<GenerateEmailConfirmationEmailQueryHandler> _logger;

        public GenerateEmailConfirmationEmailQueryHandler(IMediator mediator, INotificationService notificationService, IConfiguration configuration, UserManager<ApplicationUser> userManager, ILogger<GenerateEmailConfirmationEmailQueryHandler> logger)
        {
            _mediator = mediator;
            _notificationService = notificationService;
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<string> Handle(GenerateEmailConfirmationEmailQuery request, CancellationToken cancellationToken)
        {
            var user = _mediator.Send(new GetUserByEmailQuery(request.Email), cancellationToken).Result;

            if (user == null)
            {
                throw new InvalidUserException();
            }

            if (await _userManager.IsEmailConfirmedAsync(user))
            {
                throw new EmailIsAlreadyConfirmedException();
            }
            
            var token = _mediator.Send(new GenerateEmailConfirmationTokenQuery(user), cancellationToken).Result;

            await _notificationService.SendNotificationAsync(toName: request.Email, toEmailAddress: request.Email, subject: "Confirm your account",
                message: $"In order to login you must confirm your account. To continue click <a href='{_configuration["FrontEndUrl"]}/User/ConfirmEmail?userId={Base64UrlEncoder.Encode(user.Id)}&token={Base64UrlEncoder.Encode(token)}'>here</a>");
            
            _logger.LogInformation($"Generate Confirmation Email: {request.Email}: Sent confirmation email");
            return await Task.FromResult(token);
        }
    }
}