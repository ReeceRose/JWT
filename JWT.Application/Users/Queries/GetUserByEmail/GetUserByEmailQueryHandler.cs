﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JWT.Application.Users.Queries.GetUserByEmail
{
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, IdentityUser>
    {
        private readonly IdentityDbContext _context;

        public GetUserByEmailQueryHandler(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task<IdentityUser> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken: cancellationToken);
        }
    }
}
