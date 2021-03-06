﻿using System.Threading;
using System.Threading.Tasks;
using JWT.Application.Interfaces;
using JWT.Application.User.Query.GenerateResetPassword.Token;
using JWT.Application.User.Query.GetUserByEmail;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace JWT.Application.User.Query.GenerateResetPassword.Email
{
    public class GenerateResetPasswordEmailQueryHandler : IRequestHandler<GenerateResetPasswordEmailQuery, string>
    {
        private readonly IMediator _mediator;
        private readonly INotificationService _notificationService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<GenerateResetPasswordEmailQueryHandler> _logger;

        public GenerateResetPasswordEmailQueryHandler(IMediator mediator, INotificationService notificationService, IConfiguration configuration, ILogger<GenerateResetPasswordEmailQueryHandler> logger)
        {
            _mediator = mediator;
            _notificationService = notificationService;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> Handle(GenerateResetPasswordEmailQuery request, CancellationToken cancellationToken)
        {
            var user = _mediator.Send(new GetUserByEmailQuery(request.Email), cancellationToken).Result;

            if (user == null)
            {
                return await Task.FromResult<string>(null);
            }
            _logger.LogInformation($"Generate Reset Password Email: {request.Email}: Requesting email");

            var token = _mediator.Send(new GenerateResetPasswordTokenQuery(user), cancellationToken).Result;

            await _notificationService.SendNotificationAsync(toName: request.Email, toEmailAddress: request.Email, subject: "Password reset",
                message: $"You have requested a password reset. To reset our password click <a href='{_configuration["FrontEndUrl"]}/User/ResetPassword?email={user.Email}&token={Base64UrlEncoder.Encode(token)}'>here</a>");
            _logger.LogInformation($"Generate Reset Password Email: {request.Email}: Sent email");
            return await Task.FromResult(token);
        }
    }
}