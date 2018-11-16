using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using JWT.Application.Users.Models;
using JWT.Application.Users.Queries;
using Microsoft.EntityFrameworkCore;

namespace Core.Queries.Users
{
    public class GetUserHandler : IRequestHandler<GetUserByEmailQuery, ApplicationUserDto>
    {
        private readonly IdentityDbContext _context;
        private readonly IMapper _mapper;

        public GetUserHandler(IdentityDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApplicationUserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<ApplicationUserDto>(await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken));
        }
    }
}
