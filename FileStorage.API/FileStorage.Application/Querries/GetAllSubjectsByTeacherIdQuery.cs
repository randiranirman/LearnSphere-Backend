using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using MediatR;

namespace FileStorage.Application.Querries
{
    public record GetAllSubjectsByTeacherIdQuery(int TeacherId) : IRequest<IEnumerable<SubjectsDTO?>>;

    public class GetAllSubjectsByTeacherIdQueryHandler(ITeacherFilesRepository teacherFilesRepository)
        : IRequestHandler<GetAllSubjectsByTeacherIdQuery, IEnumerable<SubjectsDTO?>>
    {
        public async Task<IEnumerable<SubjectsDTO?>> Handle(GetAllSubjectsByTeacherIdQuery request, CancellationToken cancellationToken)
        {
            return await teacherFilesRepository.GetAllSubjectsByTeacherId(request.TeacherId);
        }
    }
}
