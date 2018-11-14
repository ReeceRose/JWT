using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Policy = "AdministratorOnly")]
    //[Authorize(Roles = "Administrator")]

    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpGet("Users")]
        public IActionResult GetUsers()
        {
            return Ok();
        }
    }
}