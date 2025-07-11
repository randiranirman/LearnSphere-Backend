using CourseRegistration.Application.Dtos;
using CourseRegistration.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseRegistration.API.Controllers
{
    [Route("classes/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;
        public ClassController(IClassService classService)
        {

            _classService = classService;
            
        }

        [HttpPost("create-class")]

        public async Task<IActionResult> CreateClassAsync(CreateClassRequset request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var createdClass = await _classService.CreateClassAsync(request);
            return Ok(createdClass);
        }
    }
}
