using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using MediatR;

namespace FileStorage.Application.Querries
{
    public record GetAllSubmissionsByAssignmentIdQuery(int AssignmentId) : IRequest<IEnumerable<SubmissionsByAssignmentIdResponseDTO>>;

    public class GetAllSubmissionsByAssignmentIdQueryHandler(ITeacherFilesRepository teacherFilesRepository)
        : IRequestHandler<GetAllSubmissionsByAssignmentIdQuery, IEnumerable<SubmissionsByAssignmentIdResponseDTO>>
    {
        public async Task<IEnumerable<SubmissionsByAssignmentIdResponseDTO>> Handle(GetAllSubmissionsByAssignmentIdQuery request, CancellationToken cancellationToken)
        {
            return await teacherFilesRepository.GetAllSubmissionsByAssignmentId(request.AssignmentId);
        }
    }
}
