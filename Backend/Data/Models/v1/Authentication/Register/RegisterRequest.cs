namespace Data.Models.v1.Authentication.Register
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
