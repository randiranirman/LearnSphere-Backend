using AnalyticsService.Application.DTOs;
using AnalyticsService.Application.Interfaces;
using MediatR;

namespace AnalyticsService.Application.Querries
{
    public record GetAllStudentsByAssignmentIdQuery(int AssignmentId) : IRequest<IEnumerable<StudentsByAssignmentIdResponseDTO>>;

    public class GetAllStudentsByAssignmentIdQueryHanler(IAssignmentsRepository assignmentsRepository)
        : IRequestHandler<GetAllStudentsByAssignmentIdQuery, IEnumerable<StudentsByAssignmentIdResponseDTO>>
    {
        public async Task<IEnumerable<StudentsByAssignmentIdResponseDTO>> Handle(GetAllStudentsByAssignmentIdQuery request, CancellationToken cancellationToken)
        {
            return await assignmentsRepository.GetAllStudentsByAssignmentIdAsync(request.AssignmentId);
        }
    }
}
