using System.Threading;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using JWT.Application.Interfaces;
using JWT.Application.User.Query.GenerateEmailConfirmation.Token;
using JWT.Application.User.Query.GetUserByEmail;
using JWT.Domain.Entities;
using JWT.Domain.Exceptions;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public GenerateEmailConfirmationEmailQueryHandler(IMediator mediator, INotificationService notificationService, IConfiguration configuration, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _mediator = mediator;
            _notificationService = notificationService;
            _configuration = configuration;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<string> Handle(GenerateEmailConfirmationEmailQuery request, CancellationToken cancellationToken)
        {
            var user = _mediator.Send(new GetUserByEmailQuery(request.Email), cancellationToken).Result;

            if (user == null)
            {
                throw new InvalidUserException();
            }

            if (await _userManager.IsEmailConfirmedAsync(_mapper.Map<ApplicationUser>(user)))
            {
                throw new EmailIsAlreadyConfirmedException();
            }

            var token = _mediator.Send(new GenerateEmailConfirmationTokenQuery(user), cancellationToken).Result;

            await _notificationService.SendNotificationAsync(toName: request.Email, toEmailAddress: request.Email, subject: "Confirm your account",
                message: $"In order to login you must confirm your account. To continue click <a href='{_configuration["FrontEndUrl"]}/User/ConfirmEmail?userId={HttpUtility.UrlEncode(user.Id)}&token={HttpUtility.UrlEncode(token)}'>here</a>");
            
            return await Task.FromResult(token);
        }
    }
}