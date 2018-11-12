using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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
        private readonly UserService _userService;

        public AuthenticationController(SignInManager<IdentityUser> signInManager, UserService userService)
        {
            _signInManager = signInManager;
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginData loginData)
        {

            var user = await _userService.GetUserByUsernameAsync(loginData.Username);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginData.Password, true);


            //var securityKey = GET CONFIG SECURITY KEY
            var securityKey = "PLACE YOUR KEY HERE";
            var symmetricSecutiyKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            var signingCredentials = new SigningCredentials(symmetricSecutiyKey, SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, "Administrator"));

            var token = new JwtSecurityToken(
                issuer: "Issuer",
                audience: "Audience",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredentials
            );

            
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}