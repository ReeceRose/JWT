using System.Threading.Tasks;
using JWT.Application.User.Query.GetAllUsers;
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
        [HttpGet("All")]
        public async Task<IActionResult> GetAllUsers() { return Ok(new { result = await _mediator.Send(new GetAllUsersQuery()) }); }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAUserById([FromQuery] string id) { return Ok(new { result = await _mediator.Send(new GetAllUsersQuery()) }); }

        [HttpGet("Count")]
        public async Task<IActionResult> GetUserCountAsync() { return Ok(new { result = await _mediator.Send(new GetUserCountQuery()) }); }
    }
}