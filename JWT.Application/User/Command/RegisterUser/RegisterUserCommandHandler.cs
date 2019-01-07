using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Application.User.Command.CreateUser;
using JWT.Application.User.Model;
using JWT.Application.User.Query.GenerateEmailConfirmation.Email;
using JWT.Application.User.Query.GetUserByEmail;
using JWT.Domain.Exceptions;
using MediatR;

namespace JWT.Application.User.Command.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var email = request.Email;
            var user = _mediator.Send(new GetUserByEmailQuery(email), cancellationToken).Result;

            if (user != null)
            {
                throw new AccountAlreadyExistsException(email);
            }

            user = _mapper.Map<ApplicationUserDto>(request);

            var result = await _mediator.Send(new CreateUserCommand(user, request.Password), cancellationToken);
            if (!result.Succeeded)
            {
                throw new InvalidRegisterException();
            }

            await _mediator.Send(new GenerateEmailConfirmationEmailQuery(email), cancellationToken);

            return await Task.FromResult(result.Succeeded);
        }
    }
}