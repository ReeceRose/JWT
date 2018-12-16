using MediatR;

namespace JWT.Application.User.Command.ConfirmUserEmail
{
    public class ConfirmUserEmailCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}