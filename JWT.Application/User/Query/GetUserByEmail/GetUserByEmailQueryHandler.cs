using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Application.User.Model;
using JWT.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JWT.Application.User.Query.GetUserByEmail
{
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, ApplicationUserDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetUserByEmailQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApplicationUserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<ApplicationUserDto>(await _context.Users.FirstOrDefaultAsync(
                u => String.Equals(u.Email, request.Email, StringComparison.CurrentCultureIgnoreCase),
                cancellationToken: cancellationToken)); ;
        }
    }
}