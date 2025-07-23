using CourseRegistration.Application.Dtos;
using CourseRegistration.Application.Interfaces;
using CourseRegistration.Application.Repositories;
using CourseRegistration.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseRegistration.API.Controllers
{
    [Route("subjects/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {

        private readonly ISubjectService _subjectService;


        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
            


        }


       
        [HttpPost("create-subjects")]
        public async Task<IActionResult> CreateSubject([FromBody] CreateSubjectRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var subject = await _subjectService.CreatedSubjectAsync(request);
                return Ok(subject); // or CreatedAtAction if you're returning a 201
            }
            catch (ApplicationException ex)
            {
                return Conflict(new { message = ex.Message }); // 409 Conflict for duplicate
            }
            catch (Exception ex)
            {
                // For other unexpected errors
                return StatusCode(500, new { message = "An error occurred while creating the subject." });
            }
        }
        [HttpGet("getAllSubjects")]
        public async Task<IActionResult> GetAllSubjectsAsync()

        {

            var allSubjects = await _subjectService.GetAllSubjectsAsync();
            return Ok(allSubjects);




        }


        [Authorize(Roles ="admin")]
        [HttpGet("getSubjectById/{id}")]

        public async Task<IActionResult> GetSubjectById( int id )
        {
            var subject = await _subjectService.GetSubjectByIdAsync(id);


             if ( subject == null)
            {
                return NotFound("No subjects with the id ");

            }
             return Ok(subject);
        }

        //[Authorize(Roles ="admin")]
        [HttpPut("subject/update/{id}")]
        public async Task<IActionResult> updateSubjectAsync(int id, [FromBody] CreateSubjectRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var updatedSubject = await _subjectService.UpdateSubjectAsync(id, request);

                if (updatedSubject == null)
                {
                    return NotFound(new { message = "Subject not found." });
                }

                return Ok(updatedSubject);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the subject." });
            }
        }

        [Authorize(Roles ="teacher")]
        [HttpGet("getSubjecstByTeacherId/{teacherId}")]
        public async Task<IActionResult> GetSubjectByTeahcerIDAsync( int teacherId)
        {
            var subjects= await _subjectService.GetSubjectsByTeacherIdAsync(teacherId);
            if( subjects == null)
            {
                return NotFound("subject not found invalid teacher id ");
            }

            return  Ok(subjects);
        }

        [Authorize(Roles ="student")]
        [HttpGet("getSubjectsByStudentId/{studentId}")]

        public  async Task<IActionResult> GetSubjectsByStudentIDAsync( int studentId)
        {
            var subjects = await _subjectService.GetSubjectsByStudentIdAsync(studentId);


            if( subjects == null)
            {
                return NotFound("invalid student id ");
            }


            return Ok(subjects);
        }

        
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            try
            {
                var result = await _subjectService.DeleteSubjectAsync(id);
                
                if (!result)
                {
                    return NotFound(new { message = "Subject not found or could not be deleted." });
                }
                
                return Ok(new { message = "Subject deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the subject." });
            }
        }


       
        [Authorize(Roles ="admin")]
        [HttpPost("clear-cache")]
        public async Task<IActionResult> ClearSubjectCache()
        {
            try
            {
                var cacheService = HttpContext.RequestServices.GetRequiredService<ICacheService>();
                
                // Clear all subject-related cache
                await cacheService.RemoveByPatternAsync("subject*");
                
                return Ok(new { message = "All subject cache cleared successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while clearing cache.", error = ex.Message });
            }
        }


    }
}
