using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Application.User.Model;
using JWT.Domain.Entities;
using JWT.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JWT.Application.User.Query.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<ApplicationUserDto>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<ApplicationUserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _dbContext.Users.ToListAsync(cancellationToken: cancellationToken);
            var mappedUsers = _mapper.Map<List<ApplicationUserDto>>(users);
//            var users = _mapper.Map<List<ApplicationUserDto>>(await _dbContext.Users.ToListAsync(cancellationToken: cancellationToken));
//            return users;
            return mappedUsers;
//            return Task.FromResult(users);
//            return Task.FromResult(_mapper.Map<List<ApplicationUserDto>>(_dbContext.Users.ToListAsync(cancellationToken: cancellationToken)));


//            return _mapper.Map<List<ApplicationUserDto>, List<ApplicationUser>>(_dbContext.Users.ToList());
//            return Task.FromResult(_dbContext.Users.ToList());
//            return _mapper.Map<List<ApplicationUserDto>>(await _dbContext.Users.ToListAsync().Result);
        }
    }
}
