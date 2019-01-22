﻿using System.Threading;
using System.Threading.Tasks;
using JWT.Application.User.Query.GetUserById;
using JWT.Domain.Exceptions;
using JWT.Persistence;
using MediatR;

namespace JWT.Application.User.Command.DisableUser
{
    public class DisableUserCommandHandler : IRequestHandler<DisableUserCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _context;

        public DisableUserCommandHandler(IMediator mediator, ApplicationDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task<bool> Handle(DisableUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(request.UserId), cancellationToken);
            // Should not be the case as the ID is not being touched by the user at all
            if (user == null)
            {
                throw new InvalidUserException();
            }

            user.AccountEnabled = false;

            var result = _context.Users.Update(user);

            await _context.SaveChangesAsync(cancellationToken);
            
            return await Task.FromResult(result.Entity.AccountEnabled);
        }
    }
}
