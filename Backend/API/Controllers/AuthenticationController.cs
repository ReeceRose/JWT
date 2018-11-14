using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<IActionResult> LoginAsync([FromBody]AccountData accountData)
        {

            var user = await _userService.GetUserByUsernameAsync(accountData.Email);

            if (user == null)
            {
                // Eventually you might want to replace these with your own methods such as BadLoginAttempt and then you can furhter what you return
                return BadRequest(new { message = "Bad login attempt"});
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, accountData.Password, true);

            if (!result.Succeeded)
            {
                return BadRequest(new { message = "Bad login attempt" });
            }

            return Ok(new
            {
                Username = user.UserName,
                Token = GenerateTokenAsync(user).Result
            });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody]AccountData accountData)
        {
            var user = await _userService.GetUserByUsernameAsync(accountData.Email);

            if (user != null)
            {
                return BadRequest(new { message = "User already exists" });
            }

            user = new IdentityUser { UserName = accountData.Email, Email = accountData.Email };

            var result = await _userManager.CreateAsync(user, accountData.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new {message = "Error logging in"});
            }

            if (accountData.IsAdmin)
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