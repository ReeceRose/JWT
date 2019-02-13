using System.Threading.Tasks;
using JWT.Application.User.Command.AddUserClaim;
using JWT.Application.User.Command.ConfirmUserEmail;
using JWT.Application.User.Command.DisableUser;
using JWT.Application.User.Command.EnableUser;
using JWT.Application.User.Command.ForceEmailConfirmation;
using JWT.Application.User.Command.RemoveUser;
using JWT.Application.User.Command.RemoveUserClaim;
using JWT.Application.User.Command.ResetPassword;
using JWT.Application.User.Query.GenerateEmailConfirmation.Email;
using JWT.Application.User.Query.GenerateResetPassword.Email;
using JWT.Application.User.Query.GetAllUsersPaginated;
using JWT.Application.User.Query.GetAUserById;
using JWT.Application.User.Query.GetUserById;
using JWT.Application.User.Query.GetUserClaim;
using JWT.Application.User.Query.GetUserCount;
using JWT.Application.User.Query.SearchUsersByEmail;
using JWT.Domain.Entities;
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

        [HttpPost]
        [Authorize(Policy = "AdministratorOnly")]
        public async Task<IActionResult> PostAllUsersAsync([FromBody] PaginationModel model) => Ok(new { result = await _mediator.Send(new GetAllUsersPaginatedQuery(model)) });


        [HttpGet("Count")]
        [Authorize(Policy = "AdministratorOnly")]
        public async Task<IActionResult> GetUserCountAsync() => Ok(new { result = await _mediator.Send(new GetUserCountQuery()) });

        // User specific actions

        [HttpGet("{UserId}/Details")]
        [Authorize(Policy = "AdministratorOnly")]
        public async Task<IActionResult> GetUserDetailsByIdAsync(string userId)
        {
            var user = await _mediator.Send(new GetAUserByIdQuery(userId));
            return Ok(new { result = new { user, claims = await _mediator.Send(new GetUserClaimQuery(user)) }});
        }

        [HttpGet("Search/{Email}")]
        [Authorize(Policy = "AdministratorOnly")]
        public async Task<IActionResult> GetUsersDetailsByEmailAsync(string email) => Ok(new { result = await _mediator.Send(new SearchUsersByEmailQuery(email)) });

        [HttpGet("{UserId}/ForceEmailConfirmation")]
        [Authorize(Policy = "AdministratorOnly")]
        public async Task<IActionResult> GetForceEmailConfirmationAsync(string userId) => Ok(new { result = await _mediator.Send(new ForceEmailConfirmationCommand(userId)) });

        [HttpGet("{UserId}/Enable")]
        [Authorize(Policy = "AdministratorOnly")]
        public async Task<IActionResult> GetEnableAUserByIdAsync(string userId) => Ok(new { result = await _mediator.Send(new EnableUserCommand(userId)) });

        [HttpGet("{UserId}/Disable")]
        [Authorize(Policy = "AdministratorOnly")]
        public async Task<IActionResult> GetDisableAUserByIdAsync(string userId) => Ok(new { result = await _mediator.Send(new DisableUserCommand(userId)) });

        [HttpGet("{UserId}/Delete")]
        [Authorize(Policy = "AdministratorOnly")]
        public async Task<IActionResult> GetRemoveUserAsync(string userId) => Ok(new { result = await _mediator.Send(new RemoveUserCommand(userId)) });

        [HttpPost("{UserId}/AddClaim/{Claim}")]
        [Authorize(Policy = "AdministratorOnly")]
        public async Task<IActionResult> PostAddClaimAsync(string userId, string claim) => Ok( new { result = await _mediator.Send(new AddUserClaimCommand(await _mediator.Send(new GetUserByIdQuery(userId)), claim, "")) });

        [HttpPost("{UserId}/RemoveClaim/{Claim}")]
        [Authorize(Policy = "AdministratorOnly")]
        public async Task<IActionResult> PostRemoveClaimAsync(string userId, string claim) => Ok( new { result = await _mediator.Send(new RemoveUserClaimCommand(await _mediator.Send(new GetUserByIdQuery(userId)), claim))} );
        
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