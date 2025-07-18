using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using FileStorage.Domain.Entities;
using MediatR;

namespace FileStorage.Application.Commands
{
    public record EditSubjectTopicCommand(int TopicId, string NewTopicName) : IRequest<SubjectTopicDTO?>;

    public class EditSubjectTopicCommandHandler(ITeacherFilesRepository teacherFilesRepository)
        : IRequestHandler<EditSubjectTopicCommand, SubjectTopicDTO?>
    {
        public async Task<SubjectTopicDTO?> Handle(EditSubjectTopicCommand request, CancellationToken cancellationToken)
        {
            var response = await teacherFilesRepository.EditSubjectTopic(request.TopicId, request.NewTopicName);
            if (response is null)
            {
                return null;
            }
            return response;
        }
    }

}
