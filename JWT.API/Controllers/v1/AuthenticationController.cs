using System.Threading.Tasks;
using JWT.Application.User.Command.ConfirmUserEmail;
using JWT.Application.User.Command.RegisterUser;
using JWT.Application.User.Command.ResetPassword;
using JWT.Application.User.Query.GenerateEmailConfirmation.Email;
using JWT.Application.User.Query.GenerateResetPassword.Email;
using JWT.Application.User.Query.LoginUser;
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

        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> PostConfirmEmail([FromBody] ConfirmUserEmailCommand confirmUserEmailCommand) => Ok(new { result = await _mediator.Send(confirmUserEmailCommand) });

        [HttpPost("GenerateConfirmationEmail")]
        public async Task<IActionResult> PostRegenerateConfirmationEmail([FromBody] GenerateEmailConfirmationEmailQuery regenerateConfirmationEmailCommand) => Ok(new { result = await _mediator.Send(regenerateConfirmationEmailCommand) });

        [HttpPost("GenerateResetPasswordEmail")]
        public async Task<IActionResult> PostGenerateRestResetPasswordEmail([FromBody] GenerateResetPasswordEmailQuery generateResetPasswordEmailQuery) => Ok(new { result = await _mediator.Send(generateResetPasswordEmailQuery) });

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> PostResetPassword([FromBody] ResetPasswordCommand resetPasswordCommand) => Ok(new { result = await _mediator.Send(resetPasswordCommand) });
    }
}