using JWT.Domain.Entities;
using MediatR;

namespace JWT.Application.User.Query.GetUserById
{
    public class GetUserByIdQuery : IRequest<ApplicationUser>
    {
        public GetUserByIdQuery(string userId) => UserId = userId;
        public string UserId { get; set; }
    }
}
