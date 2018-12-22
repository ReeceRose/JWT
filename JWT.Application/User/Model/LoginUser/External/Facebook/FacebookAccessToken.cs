using Newtonsoft.Json;

namespace JWT.Application.User.Model.LoginUser.External.Facebook
{
    public class FacebookAccessToken
    {
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
