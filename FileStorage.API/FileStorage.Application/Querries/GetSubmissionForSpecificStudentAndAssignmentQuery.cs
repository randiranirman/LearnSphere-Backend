using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using MediatR;

namespace FileStorage.Application.Querries
{
    public record GetSubmissionForSpecificStudentAndAssignmentQuery(int AssignmentId, int StudentId) : IRequest<SubmissionDTO?>;

    public class GetSubmissionForSpecificStudentAndAssignmentQueryHandler(IStudentFilesRepository studentFilesRepository)
        : IRequestHandler<GetSubmissionForSpecificStudentAndAssignmentQuery, SubmissionDTO?>
    {
        public async Task<SubmissionDTO?> Handle(GetSubmissionForSpecificStudentAndAssignmentQuery request, CancellationToken cancellationToken)
        {
            var response = await studentFilesRepository.GetSubmissionForSpecificStudentAndAssignment(request.AssignmentId, request.StudentId);
            if (response is null) return null;
            return response;
        }
    }
}
