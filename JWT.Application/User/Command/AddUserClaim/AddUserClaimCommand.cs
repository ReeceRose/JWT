using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JWT.Application.User.Command.AddUserClaim
{
    public class AddUserClaimCommand : IRequest<bool>
    {
        public AddUserClaimCommand(IdentityUser user, string key, string value)
        {
            User = User;
            Key = key;
            Value = value;
        }

        public IdentityUser User { get; set; }
        public string Key { get; }
        public string Value { get; }
    }
}
