using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using MediatR;

namespace FileStorage.Application.Commands
{
    public record DeleteSubjectTopicByTopicIdCommand(int TopicId) : IRequest<SubjectTopicDTO?>;

    public class DeleteSubjectTopicByTopicIdCommandHandler(ITeacherFilesRepository teacherFilesRepository)
        : IRequestHandler<DeleteSubjectTopicByTopicIdCommand, SubjectTopicDTO?>
    {
        public async Task<SubjectTopicDTO?> Handle(DeleteSubjectTopicByTopicIdCommand request, CancellationToken cancellationToken)
        {
            return await teacherFilesRepository.DeleteSubjectTopicByTopicId(request.TopicId);
        }
    }
}
