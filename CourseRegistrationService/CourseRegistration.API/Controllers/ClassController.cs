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
        [HttpDelete("deleteClassById/{id}")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            var result = await _classService.DeleteClassByIdAsync(id);

           
            if (!result)
                return NotFound($"Class with ID {id} not found.");

            return Ok("class deleted ");
        }



        [HttpGet("getAllClasses")]
        public async Task<IActionResult> GetAllClassesAsync()
        {
            var classes = await _classService.GetAllClassesAsync();
            

            if (classes == null)
            {
                return NotFound("No classes found.");
            }

            return Ok(classes);

        }
        





    }
}
