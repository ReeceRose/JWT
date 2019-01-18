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
    public class AdminController : ControllerBase
    {
        // If the user is authenticated this will simply just return Ok, if not return an error
        [HttpGet("Verify")]
        public IActionResult GetVerifyIsAdmin() { return Ok(new { result = true }); }
    }
}