using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWT.Application.User.Command.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IdentityResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userManager.CreateAsync(_mapper.Map<ApplicationUser>(request.User), request.Password);
        }
    }
}
