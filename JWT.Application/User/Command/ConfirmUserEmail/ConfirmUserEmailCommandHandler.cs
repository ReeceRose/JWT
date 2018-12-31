using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Application.User.Query.GetUserById;
using JWT.Domain.Entities;
using JWT.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWT.Application.User.Command.ConfirmUserEmail
{
    public class ConfirmUserEmailCommandHandler : IRequestHandler<ConfirmUserEmailCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public ConfirmUserEmailCommandHandler(IMediator mediator, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _mediator = mediator;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<bool> Handle(ConfirmUserEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(request.UserId), cancellationToken);
            if (user == null)
            {
                throw new InvalidUserException();
            }

            var result = await _userManager.ConfirmEmailAsync(_mapper.Map<ApplicationUser>(user), request.Token);

            return await Task.FromResult(result.Succeeded);
        }
    }
}
