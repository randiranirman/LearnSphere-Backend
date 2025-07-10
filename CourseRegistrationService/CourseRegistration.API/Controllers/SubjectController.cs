using CourseRegistration.Application.Dtos;
using CourseRegistration.Application.Interfaces;
using CourseRegistration.Application.Repositories;
using CourseRegistration.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseRegistration.API.Controllers
{
    [Route("Subjects/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {

        private readonly ISubjectService _subjectService;


        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
            


        }

        [HttpPost("create-subjects")]
        public async Task<IActionResult> CreateSubject( CreateSubjectRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdSubject = await _subjectService.CreatedSubjectAsync(request);


            return Ok(createdSubject);


        }

        [HttpGet("getAllSubjects")]
        public async Task<IActionResult> GetAllSubjectsAsync()

        {

            var allSubjects = await _subjectService.GetAllSubjectsAsync();
            return Ok(allSubjects);




        }

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
    }
}
