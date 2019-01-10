using System;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT.API.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public HomeController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public IActionResult Index()
        {
            var token = _antiforgery.GetTokens(HttpContext).RequestToken;
            HttpContext.Response.Cookies.Append("XSRF-TOKEN", token, new CookieOptions { HttpOnly = false });
            return new StatusCodeResult(StatusCodes.Status200OK);
            //            var token = _antiforgery.GetTokens(HttpContext).RequestToken;
            ////            HttpContext.Response.Cookies.Append("XSRF-TOKEN", token, new CookieOptions { HttpOnly = false });
            //            return Ok(new { token = _antiforgery.GetTokens(HttpContext).RequestToken });
            //            var token = _antiforgery.GetTokens(HttpContext).RequestToken;
            //            HttpContext.Response.Cookies.Append("XSRF-TOKEN", token, new CookieOptions
            //            {
            //                Path = "/",
            //                Domain = "localhost",
            //                Expires = DateTime.UtcNow.AddHours(6),
            //                HttpOnly = false,
            //                Secure = false
            //            });
            //            return new StatusCodeResult(StatusCodes.Status200OK);
        }
    }
}