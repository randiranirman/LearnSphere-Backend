using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using MediatR;

namespace FileStorage.Application.Querries
{
    public record GetAllAssignmentsBySubjectIdQuery(int SubjectId) : IRequest<IEnumerable<AssignmentDTO>>;

    public class GetAllAssignmentsBySubjectIdQueryHandler(ITeacherFilesRepository teacherFilesRepository)
        : IRequestHandler<GetAllAssignmentsBySubjectIdQuery, IEnumerable<AssignmentDTO>>
    {
        public async Task<IEnumerable<AssignmentDTO>> Handle(GetAllAssignmentsBySubjectIdQuery request, CancellationToken cancellationToken)
        {
            return await teacherFilesRepository.GetAllAssignmentsBySubjectId(request.SubjectId);
        }
    }


}
