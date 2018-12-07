using MediatR;

namespace JWT.Application.Users.Commands.ConfirmUserEmail
{
    public class ConfirmUserEmailCommand : IRequest<bool>
    {
        public string Code { get; set; }
    }
}
