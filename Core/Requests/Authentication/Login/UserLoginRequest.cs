using MediatR;

namespace Core.Requests.Authentication.Login
{
    public class UserLoginRequest : IRequest<bool>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}