using AutoMapper;
using CourseManagementService.Models.DTOs;
using CourseManagementService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherSubjectController : ControllerBase
    {
        private readonly ITeacherSubjectRepository teacherSubjectRepository;
        private readonly IMapper mapper;

        public TeacherSubjectController(ITeacherSubjectRepository teacherSubjectRepository, IMapper mapper)
        {
            this.teacherSubjectRepository = teacherSubjectRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeacherSubjects()
        {
            var teacherSubjectDomainModel = await teacherSubjectRepository.GetTeacherSubjectAsync();

            // TeacherSubject(Domain model) => TeacherSubjectDTO
            var prevResponesType = mapper.Map<List<TeacherSubjectDTO>>(teacherSubjectDomainModel);

            // TeacherSubject(Domain model) => CreatedSubjectDTO
            var newResponseTypeForFrontend = mapper.Map<List<CreatedSubjectDTO>>(teacherSubjectDomainModel); // didn't work

            return Ok(prevResponesType);
        }
    }
}
