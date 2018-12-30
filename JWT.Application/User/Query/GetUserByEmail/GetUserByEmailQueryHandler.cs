using System;
using System.Threading;
using System.Threading.Tasks;
using JWT.Domain.Entities;
using JWT.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JWT.Application.User.Query.GetUserByEmail
{
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, ApplicationUser>
    {
        private readonly ApplicationDbContext _context;

        public GetUserByEmailQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => String.Equals(u.Email, request.Email, StringComparison.CurrentCultureIgnoreCase), cancellationToken: cancellationToken);
        }
    }
}