using MediatR;

namespace JWT.Application.User.Command.AddUserClaim
{
    public class AddUserClaimCommand : IRequest<bool>
    {
        public AddUserClaimCommand(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; }
        public string Value { get; }
    }
}
