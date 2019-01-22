using System.Threading.Tasks;
using JWT.Application.User.Command.ConfirmUserEmail;
using JWT.Application.User.Command.DisableUser;
using JWT.Application.User.Command.EnableUser;
using JWT.Application.User.Command.ForceEmailConfirmation;
using JWT.Application.User.Command.RemoveUser;
using JWT.Application.User.Command.ResetPassword;
using JWT.Application.User.Query.GenerateEmailConfirmation.Email;
using JWT.Application.User.Query.GenerateResetPassword.Email;
using JWT.Application.User.Query.GetAllUsers;
using JWT.Application.User.Query.GetAUserById;
using JWT.Application.User.Query.GetUserCount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWT.API.Controllers.v1.Admin
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // TODO: Pagination
        [HttpGet]
        [Authorize(Policy = "AdministratorOnly")]
        public async Task<IActionResult> GetAllUsersAsync() { return Ok(new { result = await _mediator.Send(new GetAllUsersQuery()) }); }

        [HttpGet("Count")]
        [Authorize(Policy = "AdministratorOnly")]
        public async Task<IActionResult> GetUserCountAsync() { return Ok(new { result = await _mediator.Send(new GetUserCountQuery()) }); }

        // User specific actions

        [HttpGet("Details/{UserId}")]
        [Authorize(Policy = "AdministratorOnly")]
        public async Task<IActionResult> GetUserDetailsAsync(string userId) { return Ok(new { result = await _mediator.Send(new GetAUserByIdQuery(userId)) }); }

        [HttpGet("ForceEmailConfirmation/{UserId}")]
        [Authorize(Policy = "AdministratorOnly")]
        public async Task<IActionResult> GetForceEmailConfirmationAsync(string userId) { return Ok(new { result = await _mediator.Send(new ForceEmailConfirmationCommand(userId)) }); }

        [HttpGet("Enable/{UserId}")]
        [Authorize(Policy = "AdministratorOnly")]
        public async Task<IActionResult> GetEnableAUserByIdAsync(string userId) { return Ok(new { result = await _mediator.Send(new EnableUserCommand(userId)) }); }

        [HttpGet("Disable/{UserId}")]
        [Authorize(Policy = "AdministratorOnly")]
        public async Task<IActionResult> GetDisableAUserByIdAsync(string userId) { return Ok(new { result = await _mediator.Send(new DisableUserCommand(userId)) }); }

        [HttpGet("Delete/{UserId}")]
        [Authorize(Policy = "AdministratorOnly")]
        public async Task<IActionResult> GetRemoveUserAsync(string userId) { return Ok(new { result = await _mediator.Send(new RemoveUserCommand(userId)) }); }

        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> PostConfirmEmailAsync([FromBody] ConfirmUserEmailCommand confirmUserEmailCommand) => Ok(new { result = await _mediator.Send(confirmUserEmailCommand) });

        [HttpPost("GenerateConfirmationEmail")]
        public async Task<IActionResult> PostRegenerateConfirmationEmailAsync([FromBody] GenerateEmailConfirmationEmailQuery regenerateConfirmationEmailCommand) => Ok(new { result = await _mediator.Send(regenerateConfirmationEmailCommand) });

        [HttpPost("GenerateResetPasswordEmail")]
        public async Task<IActionResult> PostGenerateRestResetPasswordEmailAsync([FromBody] GenerateResetPasswordEmailQuery generateResetPasswordEmailQuery) => Ok(new { result = await _mediator.Send(generateResetPasswordEmailQuery) });

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> PostResetPasswordAsync([FromBody] ResetPasswordCommand resetPasswordCommand) => Ok(new { result = await _mediator.Send(resetPasswordCommand) });

    }
}