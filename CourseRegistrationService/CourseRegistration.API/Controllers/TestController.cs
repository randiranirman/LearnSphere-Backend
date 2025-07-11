using Microsoft.AspNetCore.Mvc;

namespace CourseRegistration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("cors")]
        public IActionResult TestCors()
        {
            return Ok(new { message = "CORS is working!", timestamp = DateTime.UtcNow });
        }

        [HttpOptions("cors")]
        public IActionResult OptionsCors()
        {
            return Ok();
        }
    }
}
