using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using FileStorage.Domain.Entities;
using FileStorage.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Infrastructure.Repositories
{
    public class TeacherFileRepository : ITeacherFilesRepository
    {
        private readonly ICourseHttpService _courseHttpService;
        private readonly FileStorageDbContext _dbContext;

        public TeacherFileRepository(ICourseHttpService courseHttpService, FileStorageDbContext dbContext)
        {
            _courseHttpService = courseHttpService;
            _dbContext = dbContext;
        }

        public async Task<CreateMaterialResponseDTO> CreateMaterialForTopics(int topicId, CreateMaterialRequestDTO createMaterialRequest)
        {
            var materialDomainModel = new Material
            {
                UploadLink = createMaterialRequest.UploadLink,
                SavedName = createMaterialRequest.SavedName,
                TopicId = topicId
            };
            await _dbContext.Materials.AddAsync(materialDomainModel);
            await _dbContext.SaveChangesAsync();

            return new CreateMaterialResponseDTO
            {
                MaterialId = materialDomainModel.Id,
                UploadLink = materialDomainModel.UploadLink,
                SavedName = materialDomainModel.SavedName,
                TopicId = materialDomainModel.TopicId
            };
        }

        public async Task<SubjectTopicDTO> CreateNewSubjectTopic(int subjectId, string newSubjectTopic)
        {
            var subjectTopicDomainModel = new SubjectTopic
            {
                TopicName = newSubjectTopic,
                SubjectId = subjectId
            };

            await _dbContext.SubjectTopics.AddAsync(subjectTopicDomainModel);
            await _dbContext.SaveChangesAsync();

            return new SubjectTopicDTO
            {
                Id = subjectTopicDomainModel.Id,
                TopicName = subjectTopicDomainModel.TopicName,
                SubjectId = subjectTopicDomainModel.SubjectId
            };
        }

        public async Task<MaterialDTO?> DeleteMaterialByMaterialId(int materialId)
        {
            var existingMaterialDomainModel = await _dbContext.Materials.FirstOrDefaultAsync(m => m.Id == materialId);
            
            if (existingMaterialDomainModel is null)
            {
                return null;
            }

            _dbContext.Remove(existingMaterialDomainModel);
            await _dbContext.SaveChangesAsync();

            return new MaterialDTO
            {
                Id = existingMaterialDomainModel.Id,
                SavedName = existingMaterialDomainModel.SavedName,
                UploadLink = existingMaterialDomainModel.UploadLink,
                TopicId = existingMaterialDomainModel.TopicId
            };

        }

        public async Task<SubjectTopicDTO?> EditSubjectTopic(int topicId, string newTopicName)
        {
            var existingSubjectDomainModel = await _dbContext.SubjectTopics.FirstOrDefaultAsync(t => t.Id == topicId);

            if (existingSubjectDomainModel is null)
            {
                return null;
            }

            existingSubjectDomainModel.TopicName = newTopicName;
            await _dbContext.SaveChangesAsync();
            return new SubjectTopicDTO
            {
                Id = existingSubjectDomainModel.Id,
                TopicName = existingSubjectDomainModel.TopicName,
                SubjectId = existingSubjectDomainModel.SubjectId
            };
        }

        public async Task<IEnumerable<AssignmentDTO>> GetAllAssignmentsBySubjectId(int subjectId)
        {
            var assignments = await _dbContext.Assignments
                .Where(a => a.SubjectId == subjectId)
                .Select(a => new AssignmentDTO
                {
                    Id = a.Id,
                    Title = a.Title,
                    DueTime = a.DueTime,
                    Status = a.Status,
                    SubjectId = a.SubjectId,
                    SubjectName = "" // This will need to be populated from the course service if needed
                })
                .ToListAsync();

            return assignments;
        }

        public async Task<IEnumerable<SubjectsDTO?>> GetAllSubjectsByTeacherId(int teacherId)
        {
            return await _courseHttpService.GetSubjectsByTeacherIdAsync(teacherId);
        }

        public async Task<IEnumerable<SubmissionsByAssignmentIdResponseDTO>> GetAllSubmissionsByAssignmentId(int assignmentId)
        {
            var response = await _dbContext.Submissions
                .Where(s => s.AssignmentId == assignmentId)
                .Select(s => new SubmissionsByAssignmentIdResponseDTO
                {
                    SubmissionId = s.Id,
                    StudentId = s.StudentId,
                    SubmissionName = s.SubmissionName,
                    SubmissionStatus = s.Status,
                    UploadLink = s.UploadLink
                })
                .ToListAsync();

            return response;
        }

        public async Task<IEnumerable<SubjectMateriealsResponseDTO>> GetSubjectMateriealsBySubjectId(int subjectId)
        {
            var response = await _dbContext.SubjectTopics
                .Where(t => t.SubjectId == subjectId)
                .Select(t => new SubjectMateriealsResponseDTO
                {
                    SubjectTopicId = t.Id,
                    SubjectTopicName = t.TopicName,
                    Materials = t.Materials
                })
                .ToListAsync();

            return response;
        }
    }
}
