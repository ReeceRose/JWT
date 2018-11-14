using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Data.Models.v1.Authentication.Login;
using Data.Models.v1.Authentication.Register;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUser _userService;

        public AuthenticationController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IUser userService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody]UserLoginRequest loginRequest)
        {

            var user = await _userService.GetUserByUsernameAsync(loginRequest.Email);

            if (user == null)
            {
                // Eventually you might want to replace these with your own methods such as BadLoginAttempt and then you can furhter what you return
                return BadRequest(new { error = "Bad login attempt"});
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, true);

            if (!result.Succeeded)
            {
                return BadRequest(new { error = "Bad login attempt" });
            }

            return Ok(new
            {
                Username = user.UserName,
                Token = GenerateTokenAsync(user).Result
            });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody]UserRegisterRequest registerRequest)
        {
            var user = await _userService.GetUserByUsernameAsync(registerRequest.Email);

            if (user != null)
            {
                return BadRequest(new { error = "User already exists" });
            }

            user = new IdentityUser { UserName = registerRequest.Email, Email = registerRequest.Email };

            var result = await _userManager.CreateAsync(user, registerRequest.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new { error = "Error logging in"});
            }

            // DO NOT DO THIS. This is for demonstration purpose only. Ideally you would make a user an admin from the admin dashboard
            if (registerRequest.IsAdmin)
            {
                await _userManager.AddClaimAsync(user, new Claim("Administrator", ""));
            }
            return Ok(new
            {
                Username = user.UserName,
                token = GenerateTokenAsync(user).Result
            });
        }

        public async Task<string> GenerateTokenAsync(IdentityUser user)
        {
            //var securityKey = GET CONFIG SECURITY KEY
            var securityKey = "PLACE YOUR KEY HERE";

            var symmetricSecutiyKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            var signingCredentials = new SigningCredentials(symmetricSecutiyKey, SecurityAlgorithms.HmacSha256Signature);

            var claims = await _userManager.GetClaimsAsync(user);
            
            var token = new JwtSecurityToken(
                issuer: "Issuer",
                audience: "Audience",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}