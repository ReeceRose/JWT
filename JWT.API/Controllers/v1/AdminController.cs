using System.Threading.Tasks;
using JWT.Application.User.Query.GetAllUsers;
using JWT.Application.User.Query.GetUserCount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWT.API.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Policy = "AdministratorOnly")]
    [Produces("application/json")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // If the user is authenticated this will simply just return Ok, if not return an error
        [HttpGet("Verify")]
        public IActionResult GetVerifyIsAdmin() { return Ok(new { result = true }); }

        // TODO: Pagination
        [HttpGet("Users")]
        public async Task<IActionResult> GetAllUsers() { return Ok(new { result = await _mediator.Send(new GetAllUsersQuery() )}); }

        [HttpGet("Users/Count")]
        public async Task<IActionResult> GetUserCountAsync() { return Ok(new { result = await _mediator.Send(new GetUserCountQuery())}); }
    }
}