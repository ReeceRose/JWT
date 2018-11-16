using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Models.Transfer;
using Core.Queries.Users;
using Core.Requests.Authentication.Login;
using Core.Requests.Authentication.Register;
using MediatR;
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
        //TODO: Refactor these and move them into the correct MediatR handler
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMediator _mediatr;
        private readonly IMapper _mapper;

        public AuthenticationController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IMediator mediatr, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mediatr = mediatr;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequest loginRequest)
        {
            // TODO: Fix
            ApplicationUser result;
            try
            {
                result = await _mediatr.Send(new GetUserByEmailQuery(loginRequest.Email));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterRequest registerRequest)
        {
            //TODO: Fix
            var result = await _mediatr.Send(registerRequest);
            return Ok();

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