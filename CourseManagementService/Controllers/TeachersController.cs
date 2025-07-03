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
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherRepository teacherRepository;
        private readonly IMapper mapper;

        public TeachersController(ITeacherRepository teacherRepository, IMapper mapper)
        {
            this.teacherRepository = teacherRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeachers()
        {
            var teacherDomainModel = await teacherRepository.GetAllAsync();

            return Ok(mapper.Map<List<TeacherDTO>>(teacherDomainModel));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacher([FromBody] AddTeacherRequestDTO addTeacherRequestDTO)
        {
            var teacherDomainModel = mapper.Map<Teacher>(addTeacherRequestDTO);

            teacherDomainModel = await teacherRepository.CreateAsync(teacherDomainModel);

            var teacherDTOModel = mapper.Map<TeacherDTO>(teacherDomainModel);
            return Ok(teacherDTOModel);
        }

        [HttpPut("{Id:guid}")]
        public async Task<IActionResult> UpdateTeacher(Guid Id, [FromBody] UpdateTeacherRequestDTO updateTeacherRequestDTO)
        {
            var existingTeacherDomainModel = mapper.Map<Teacher>(updateTeacherRequestDTO);
            existingTeacherDomainModel = await teacherRepository.UpdateAsync(Id, existingTeacherDomainModel);

            if (existingTeacherDomainModel is null)
                return NotFound();

            var existingTeacherDTOModel = mapper.Map<TeacherDTO>(existingTeacherDomainModel);

            return Ok(existingTeacherDTOModel);
        }

        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> DeleteTeacher([FromRoute] Guid Id)
        {
            var existingTeacher = await teacherRepository.DeleteAsync(Id);

            if (existingTeacher is null)
                return NotFound();

            return Ok(existingTeacher);
        }
    }
}
