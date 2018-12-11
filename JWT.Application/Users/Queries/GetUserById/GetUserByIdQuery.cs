using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWT.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<IdentityUser>
    {
        public GetUserByIdQuery(string userId) => UserId = userId;
        public string UserId { get; set; }
    }
}
