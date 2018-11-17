using System.Threading.Tasks;
using JWT.Application.Users.Commands.LoginUser;
using JWT.Application.Users.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JWT.API.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserCommand loginUserCommand)
        {
            return Ok(await _mediator.Send(loginUserCommand));
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserCommand registerUserCommand)
        {
            return Ok(await _mediator.Send(registerUserCommand));
        }

        //public async Task<string> GenerateTokenAsync(IdentityUser user)
        //{
        //    //var securityKey = GET CONFIG SECURITY KEY
        //    var securityKey = "PLACE YOUR KEY HERE";

        //    var symmetricSecutiyKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

        //    var signingCredentials = new SigningCredentials(symmetricSecutiyKey, SecurityAlgorithms.HmacSha256Signature);

        //    var claims = await _userManager.GetClaimsAsync(user);
            
        //    var token = new JwtSecurityToken(
        //        issuer: "Issuer",
        //        audience: "Audience",
        //        claims: claims,
        //        expires: DateTime.Now.AddHours(1),
        //        signingCredentials: signingCredentials
        //    );
        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
    }
}