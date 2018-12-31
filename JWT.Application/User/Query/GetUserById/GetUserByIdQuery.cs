using JWT.Application.User.Model;
using MediatR;

namespace JWT.Application.User.Query.GetUserById
{
    public class GetUserByIdQuery : IRequest<ApplicationUserDto>
    {
        public GetUserByIdQuery(string userId) => UserId = userId;
        public string UserId { get; set; }
    }
}
