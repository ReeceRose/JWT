using MediatR;

namespace JWT.Application.User.Command.ConfirmUserEmail
{
    public class ConfirmUserEmailCommand : IRequest<bool>
    {
        public ConfirmUserEmailCommand(string userId, string token)
        {
            UserId = userId;
            Token = token;
        }

        public string UserId { get; set; }
        public string Token { get; set; }
    }
}