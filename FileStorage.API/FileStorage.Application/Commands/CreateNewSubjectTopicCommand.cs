using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using MediatR;

namespace FileStorage.Application.Commands
{
    public record CreateNewSubjectTopicCommand(int SubjectId, string NewSubjectTopic) : IRequest<SubjectTopicDTO>;

    public class CreateNewSubjectTopicCommandHandler(ITeacherFilesRepository teacherFilesRepository)
        : IRequestHandler<CreateNewSubjectTopicCommand, SubjectTopicDTO>
    {
        public async Task<SubjectTopicDTO> Handle(CreateNewSubjectTopicCommand request, CancellationToken cancellationToken)
        {
            return await teacherFilesRepository.CreateNewSubjectTopic(request.SubjectId, request.NewSubjectTopic);
        }
    }

}
