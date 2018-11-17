using System.Threading.Tasks;
using JWT.Application.Users.Commands.RegisterUser;
using JWT.Application.Users.Queries.LoginUser;
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
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserQuery loginUserQuery)
        {
            return Ok(await _mediator.Send(loginUserQuery));
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserCommand registerUserCommand)
        {
            return Ok(await _mediator.Send(registerUserCommand));
        }
    }
}