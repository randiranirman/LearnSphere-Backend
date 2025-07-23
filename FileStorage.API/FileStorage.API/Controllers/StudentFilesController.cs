using FileStorage.Application.Commands;
using FileStorage.Application.DTOs;
using FileStorage.Application.Querries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentFilesController(ISender sender) : ControllerBase
    {
        // get all subject materials from subject Id
        [HttpGet("files")]
        public async Task<IActionResult> GetAllSubjectMaterialsBySubjectId([FromQuery] int subjectId)
        {
            var response = await sender.Send(new GetAllSubjectMaterialsBySubjectIdQuery(subjectId));
            return Ok(response);
        }

        [HttpGet("assignments")]
        public async Task<IActionResult> GetAllSubjectAssignmentsBySubjectId([FromQuery] int subjectId)
        {
            var resposne = await sender.Send(new GetAllAssignmentsBySubjectIdQuery(subjectId));
            return Ok(resposne);
        }

        // get a submission for specific assignment Id and student Id
        [HttpGet("submissions/{studentId:int}/{assignmentId:int}")]
        public async Task<IActionResult> GetSubmissionForSpecificStudentAndAssignment([FromRoute] int studentId, [FromRoute] int assignmentId)
        {
            var response = await sender.Send(new GetSubmissionForSpecificStudentAndAssignmentQuery(assignmentId, studentId));
            if (response is null) return NotFound();

            return Ok(response);
        }

        // made a submission 
        [HttpPost("submissions/made-submissions")]
        public async Task<IActionResult> MadeSubmissionForAssignment([FromBody] MadeSubmissionByStudentRequestDTO madeSubmissionByStudentRequest)
        {
            var response = await sender.Send(new MadeSubmissionForAssignmentCommand(madeSubmissionByStudentRequest));
            return Ok(response);
        }

        // edit a submission
        [HttpPut("submissions/edit-submissions")]
        public async Task<IActionResult> EditSubmissionForAssignment([FromQuery] int submissionId, [FromBody] EditSubmissionRequestDTO editSubmissionRequest)
        {
            var response = await sender.Send(new EditSubmissionForAssignmentCommand(submissionId, editSubmissionRequest));
            if (response is null) return NotFound();
            return Ok(response);
        }

        // delete a submission
        [HttpDelete("submissions/delete-submissions")]
        public async Task<IActionResult> DeleteSubmissionBySubmissionId([FromQuery] int submissionId)
        {
            var response = await sender.Send(new DeleteSubmissionBySubmissionIdCommand(submissionId));
            if (response is null) return NotFound();
            return Ok(response);
        }
    }
}
