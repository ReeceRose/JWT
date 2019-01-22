using JWT.Application.User.Model;
using MediatR;

namespace JWT.Application.User.Query.GetAUserById
{
    public class GetAUserByIdQuery : IRequest<ApplicationUserDto>
    {
        public GetAUserByIdQuery(string userId) => UserId = userId;
        public string UserId { get; set; }
    }
}
