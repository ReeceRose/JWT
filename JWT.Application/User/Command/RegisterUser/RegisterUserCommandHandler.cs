using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Application.User.Command.CreateUser;
using JWT.Application.User.Model;
using JWT.Application.User.Query.GenerateEmailConfirmation.Email;
using JWT.Application.User.Query.GetUserByEmail;
using JWT.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JWT.Application.User.Command.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<RegisterUserCommandHandler> _logger;

        public RegisterUserCommandHandler(IMediator mediator, IMapper mapper, ILogger<RegisterUserCommandHandler> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var email = request.Email;
            var user = await _mediator.Send(new GetUserByEmailQuery(email), cancellationToken);


            if (user != null)
            {
                _logger.LogInformation($"Register User: {email}: Registration failed: User does not exist");
                throw new AccountAlreadyExistsException();
            }
            
            var mappedUser = _mapper.Map<ApplicationUserDto>(request);
            mappedUser.AccountEnabled = true;

            var result = await _mediator.Send(new CreateUserCommand(mappedUser, request.Password), cancellationToken);
            if (!result.Succeeded)
            {
                _logger.LogInformation($"Register User: {email}: Registration failed: {result.Errors}");
                throw new InvalidRegisterException();
            }

            await _mediator.Send(new GenerateEmailConfirmationEmailQuery(email), cancellationToken);

            _logger.LogInformation($"Register User: {email}: Registration successful");
            return await Task.FromResult(result.Succeeded);
        }
    }
}