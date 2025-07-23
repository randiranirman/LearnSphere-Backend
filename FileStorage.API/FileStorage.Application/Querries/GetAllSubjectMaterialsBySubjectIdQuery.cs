using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using MediatR;

namespace FileStorage.Application.Querries
{
    public record GetAllSubjectMaterialsBySubjectIdQuery(int SubjectId) : IRequest<IEnumerable<SubjectMateriealsResponseDTO>>;

    public class GetAllSubjectMaterialsBySubjectIdQueryHandler(ITeacherFilesRepository teacherFilesRepository)
        : IRequestHandler<GetAllSubjectMaterialsBySubjectIdQuery, IEnumerable<SubjectMateriealsResponseDTO>>
    {
        public async Task<IEnumerable<SubjectMateriealsResponseDTO>> Handle(GetAllSubjectMaterialsBySubjectIdQuery request, CancellationToken cancellationToken)
        {
            return await teacherFilesRepository.GetSubjectMateriealsBySubjectId(request.SubjectId);
        }
    }


}
