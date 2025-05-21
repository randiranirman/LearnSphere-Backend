using AutoMapper;
using CourseManagementService.Models.Domains;
using CourseManagementService.Models.DTOs;
using CourseManagementService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectRepository subjectRepository;
        private readonly ITeacherRepository teacherRepository;
        private readonly ITeacherSubjectRepository teacherSubjectRepository;
        private readonly IMapper mapper;

        public SubjectsController(ISubjectRepository subjectRepository, IMapper mapper, ITeacherRepository teacherRepository, ITeacherSubjectRepository teacherSubjectRepository)
        {
            this.teacherRepository = teacherRepository;
            this.subjectRepository = subjectRepository;
            this.teacherSubjectRepository = teacherSubjectRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            var subjectDomainModel = await subjectRepository.GetSubjectAsync(); // return subject list

            var subjectDTOModel = mapper.Map<List<SubjectDTO>>(subjectDomainModel);

            return Ok(subjectDTOModel); // ok -> response code = 200(success)
        }

        [HttpGet("{Code:guid}")]
        public async Task<IActionResult> GetSubjectById([FromRoute] Guid Code)
        {
            var subjectDomainModel = await subjectRepository.GetSubjectByIdAsync(Code);

            if (subjectDomainModel is null)
                return NotFound();

            return Ok(subjectDomainModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubject([FromBody] AddSubjectRequestDTO addSubjectRequestDTO,[FromQuery] Guid assignTeacherId)
        {
            var subjectDomainModel = mapper.Map<Subject>(addSubjectRequestDTO);
            subjectDomainModel = await subjectRepository.CreateSubjectAsync(subjectDomainModel, assignTeacherId);

            var assignedTeacher = await teacherRepository.GetByIdAsync(assignTeacherId);

            if (assignedTeacher == null)
            {
                return NotFound($"Teacher with ID {assignTeacherId} not found."); // 404 not found
            }

            // change DTO type for better usage of frontend
            var newTeacherSubjectDTO = new TeacherSubjectDTO
            {
                TeacherId = assignedTeacher.Id,
                SubjectCode = subjectDomainModel.Code,
                SubjectCodeNavigation = subjectDomainModel,
                Teacher = assignedTeacher
            };

            return Ok(newTeacherSubjectDTO);
        }

        [HttpDelete("{Code:guid}")]
        public async Task<IActionResult> DeleteSubject([FromRoute] Guid Code)
        {

            var subjectDomainModel = await subjectRepository.GetSubjectByIdAsync(Code);
            Console.WriteLine("Response 02 " + subjectDomainModel.Title + subjectDomainModel.Code + subjectDomainModel.Grade);

            var teacherSubjectDomainModel = await subjectRepository.DeleteSubjectAsync(Code);
            Console.WriteLine("Response from delete function: " + teacherSubjectDomainModel.SubjectCode + " " + teacherSubjectDomainModel.TeacherId);

            if (teacherSubjectDomainModel is null)
                return NotFound();

            var deletedTeacherSubjectDomainModel = new TeacherSubject
            {
                TeacherId = teacherSubjectDomainModel.TeacherId,
                SubjectCode = teacherSubjectDomainModel.SubjectCode,
                SubjectCodeNavigation = subjectDomainModel,
                Teacher = await teacherRepository.GetByIdAsync(teacherSubjectDomainModel.TeacherId)
            };

            return Ok(deletedTeacherSubjectDomainModel);
        }

        [HttpPut("{Code:guid}")]
        public async Task<IActionResult> UpdateSubjectAsync([FromRoute] Guid Code, [FromBody] UpdateSubjectRequestDTO updateSubjectRequestDTO)
        {

            var updatedSubjectDomainModel = new Subject
            {
                Title = updateSubjectRequestDTO.Title,
                Grade = updateSubjectRequestDTO.Grade
            };

            var assignedTeacherId = updateSubjectRequestDTO.AssignedTeacherId;

            updatedSubjectDomainModel = await subjectRepository.UpdateSubjectAsync(Code, updatedSubjectDomainModel, assignedTeacherId);

            var updatedTeacherSubjectDomainModel = await teacherSubjectRepository.GetTeacherSubjectById(Code);

            if (updatedSubjectDomainModel is null)
                return NotFound();

            return Ok(updatedTeacherSubjectDomainModel);
        }
    }
}
