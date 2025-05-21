using FileStorage.Domain.Entities;
using FileStorage.Domain.Interfaces;
using MediatR;

namespace FileStorage.Application.Querries
{
    public record GetTeacherByIdQuerry(int TeacherId) : IRequest<TeacherEntity>;

    public class GetTeacherByIdQuerryHandler(ITeacherRepository teacherRepository)
        : IRequestHandler<GetTeacherByIdQuerry, TeacherEntity>
    {
        public async Task<TeacherEntity?> Handle(GetTeacherByIdQuerry request, CancellationToken cancellationToken)
        {
            return await teacherRepository.GetTeacherByIdAsync(request.TeacherId);
        }
    }

}
