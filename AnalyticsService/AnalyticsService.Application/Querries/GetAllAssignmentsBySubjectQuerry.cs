using AnalyticsService.Application.DTOs;
using AnalyticsService.Application.Interfaces;
using MediatR;

namespace AnalyticsService.Application.Querries
{
    public record GetAllAssignmentsBySubjectQuerry(int SubjectId) : IRequest<IEnumerable<AssignmentDTO>>;

    public class GetAllAssignmentsBySubjectQuerryHandler(IAssignmentsRepository assignmentsRepository)
        : IRequestHandler<GetAllAssignmentsBySubjectQuerry, IEnumerable<AssignmentDTO>>
    {
        public async Task<IEnumerable<AssignmentDTO>> Handle(GetAllAssignmentsBySubjectQuerry request, CancellationToken cancellationToken)
        {
            return await assignmentsRepository.GetAllAssignmentsBySubjectIdAsync(request.SubjectId);
        }
    }
}
