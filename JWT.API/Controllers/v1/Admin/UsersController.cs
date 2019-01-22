using System.Threading.Tasks;
using JWT.Application.User.Command.DisableUser;
using JWT.Application.User.Command.EnableUser;
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
    [Authorize(Policy = "AdministratorOnly")]
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
        public async Task<IActionResult> GetAllUsersAsync() { return Ok(new { result = await _mediator.Send(new GetAllUsersQuery()) }); }

        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetAUserByIdAsync(string userId) { return Ok(new { result = await _mediator.Send(new GetAUserByIdQuery(userId)) }); }

        [HttpGet("{UserId}/Enable")]
        public async Task<IActionResult> GetEnableAUserByIdAsync(string userId) { return Ok(new { result = await _mediator.Send(new EnableUserCommand(userId)) }); }

        [HttpGet("{UserId}/Disable")]
        public async Task<IActionResult> GetDisableAUserByIdAsync(string userId) { return Ok(new { result = await _mediator.Send(new DisableUserCommand(userId)) }); }

        [HttpGet("Count")]
        public async Task<IActionResult> GetUserCountAsync() { return Ok(new { result = await _mediator.Send(new GetUserCountQuery()) }); }
    }
}