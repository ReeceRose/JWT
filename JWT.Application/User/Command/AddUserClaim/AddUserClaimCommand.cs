using JWT.Application.User.Model;
using MediatR;

namespace JWT.Application.User.Command.AddUserClaim
{
    public class AddUserClaimCommand : IRequest<bool>
    {
        public AddUserClaimCommand(ApplicationUserDto user, string key, string value)
        {
            User = user;
            Key = key;
            Value = value;
        }

        public ApplicationUserDto User { get; set; }
        public string Key { get; }
        public string Value { get; }
    }
}
