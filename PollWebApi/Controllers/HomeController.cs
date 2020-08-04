using Microsoft.AspNetCore.Mvc;

namespace PollWebApi.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        public IActionResult Get()
        {
            return Ok("PollWebApi - Version 1.0");
        }
    }
}