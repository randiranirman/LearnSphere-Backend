using FileStorage.Application.Commands;
using FileStorage.Application.DTOs;
using FileStorage.Application.Querries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherFilesController(ISender sender) : ControllerBase
    {
        [HttpGet("classes")]
        public async Task<IActionResult> GetAllSubjectsAssignedToTeacher([FromQuery] int teacherId)
        {
            var response = await sender.Send(new GetAllSubjectsByTeacherIdQuery(teacherId));
            if (response is null || !response.Any())
            {
                return NotFound("No subjects found for this teacher.");
            }
            return Ok(response);
        }

        // get all assignments by Id
        [HttpGet("assignments")]
        public async Task<IActionResult> GetAllAssignmentsBySubjectId([FromQuery] int subjectId)
        {
            var response = await sender.Send(new GetAllAssignmentsBySubjectIdQuery(subjectId));
            if (response is null || !response.Any())
            {
                return NotFound("No assignments found for this subject.");
            }
            return Ok(response);
        }

        // create an assignment
        [HttpPost("assignments/create-assignment")]
        public async Task<IActionResult> CreateAssignment([FromBody] CreateAssignmentByTeacherRequestDTO createAssignmentByTeacherRequest)
        {
            var response = await sender.Send(new CreateAssignmentByTeacherCommand(createAssignmentByTeacherRequest));
            return Ok(response);
        }

        // edit an assignment using title, uploadLink and duetime
        [HttpPut("assignments/edit-assignment")]
        public async Task<IActionResult> UpdateAssignmentByTeacher([FromQuery] int assignmentId, [FromBody] UpdateAssignmentByTeacherRequestDTO updateAssignmentByTeacherRequest)
        {
            var response = await sender.Send(new UpdateAssignmentByTeacherCommand(assignmentId, updateAssignmentByTeacherRequest));
            return Ok(response);
        }

        // delete an assignment assignment id
        [HttpDelete("assignments/delete-assignment")]
        public async Task<IActionResult> DeleteAssignmentByTeacher([FromQuery] int assignmentId)
        {
            var response = await sender.Send(new DeleteAssignmentByTeacherCommand(assignmentId));
            return Ok(response);
        }

        [HttpGet("files")]
        public async Task<IActionResult> GetAllSubjectMaterialsBySubjectId([FromQuery] int subejctId)
        {
            var response = await sender.Send(new GetAllSubjectMaterialsBySubjectIdQuery(subejctId));
            return Ok(response);
        }

        [HttpGet("submissions")]
        public async Task<IActionResult> GetAllSubmissionsByAssignmentId([FromQuery] int assignmentId)
        {
            var response = await sender.Send(new GetAllSubmissionsByAssignmentIdQuery(assignmentId));
            return Ok(response);
        }

        // create a subject topic
        [HttpPost("files/create-subject-topic")]
        public async Task<IActionResult> CreateNewSubjectTopic([FromQuery] int subjectId, [FromBody] string newSubjectTopic)
        {
            var response = await sender.Send(new CreateNewSubjectTopicCommand(subjectId, newSubjectTopic));
            return Ok(response);
        }

        // edit a subject topic
        [HttpPut("files/edit-subject-topic")]
        public async Task<IActionResult> EditSubjectTopic([FromQuery] int topicId, [FromBody] string newSubjectTopic)
        {
            var response = await sender.Send(new EditSubjectTopicCommand(topicId, newSubjectTopic));
            return Ok(response);
        }

        // create a material for a specific topic
        [HttpPost("files/create-material")]
        public async Task<IActionResult> CreateNewMaterialForSubjectTopic([FromQuery] int topicId, [FromBody] CreateMaterialRequestDTO createMaterialRequest)
        {
            var response = await sender.Send(new CreateNewMaterialCommand(topicId, createMaterialRequest));
            if (response is null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        // delete a material for a specific topic
        [HttpDelete("files/delete-material")]
        public async Task<IActionResult> DeleteMaterialById([FromQuery] int materialId)
        {
            var response = await sender.Send(new DeleteMaterialByIdCommand(materialId));
            return Ok(response);
        }
    }
}
