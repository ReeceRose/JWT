using System;
using System.Threading.Tasks;
using JWT.Application.Users.Commands.ConfirmUserEmail;
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

        public AuthenticationController(IMediator mediator) => _mediator = mediator;

        [HttpPost("Login")]
        public async Task<IActionResult> PostLoginAsync([FromBody] LoginUserQuery loginUserQuery) => Ok(new { token = await _mediator.Send(loginUserQuery) });

        [HttpPost("Register")]
        public async Task<IActionResult> PostRegisterAsync([FromBody] RegisterUserCommand registerUserCommand) => Ok(new { result = await _mediator.Send(registerUserCommand) });

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> GetConfirmEmail([FromBody] ConfirmUserEmailCommand confirmUserEmailCommand) => Ok(new { result = await _mediator.Send(confirmUserEmailCommand) });
    }
}