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

        public async Task<AssignmentDTO> CreateAssignmentByTeacher(CreateAssignmentByTeacherRequestDTO createAssignmentByTeacherRequest)
        {
            var assignmentDomainModel = new Assignment
            {
                Title = createAssignmentByTeacherRequest.AssignmentTitle,
                DueTime = createAssignmentByTeacherRequest.DueTime,
                UploadLink = createAssignmentByTeacherRequest.UploadLink,
                ClassId = createAssignmentByTeacherRequest.ClassId,
                SubjectId = createAssignmentByTeacherRequest.SubjectId
            };

            // here I implement this as a minimal requriments but it must be implement with validating  subject and class ids.

            await _dbContext.Assignments.AddAsync(assignmentDomainModel);
            await _dbContext.SaveChangesAsync();

            return new AssignmentDTO
            {
                Id = assignmentDomainModel.Id,
                Title = assignmentDomainModel.Title,
                DueTime = assignmentDomainModel.DueTime,
                UploadLink = assignmentDomainModel.UploadLink,
                Status = assignmentDomainModel.Status,
                SubjectId = assignmentDomainModel.SubjectId
            };
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

        public async Task<AssignmentDTO> DeleteAssignmentByTeacher(int assignmentId)
        {
            var existingDomainModel = await _dbContext.Assignments.FirstOrDefaultAsync(a => a.Id == assignmentId);

            if (existingDomainModel is null)
            {
                return null;
            }
            _dbContext.Assignments.Remove(existingDomainModel);
            await _dbContext.SaveChangesAsync();

            return new AssignmentDTO
            {
                Id = existingDomainModel.Id,
                Title = existingDomainModel.Title,
                DueTime = existingDomainModel.DueTime,
                UploadLink = existingDomainModel.UploadLink,
                Status = existingDomainModel.Status,
                SubjectId = existingDomainModel.SubjectId
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

        public async Task<SubjectTopicDTO?> DeleteSubjectTopicByTopicId(int topicId)
        {
            var existingDomainModel = await _dbContext.SubjectTopics.FirstOrDefaultAsync(t => t.Id == topicId);

            if (existingDomainModel is null)
            {
                return null;
            }

            _dbContext.Remove(existingDomainModel);
            await _dbContext.SaveChangesAsync();

            return new SubjectTopicDTO
            {
                Id = existingDomainModel.Id,
                TopicName = existingDomainModel.TopicName,
                SubjectId = existingDomainModel.SubjectId
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
                    UploadLink = a.UploadLink,
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

        public async Task<AssignmentDTO?> UpdateAssignmentByTeacher(int assignmentId, UpdateAssignmentByTeacherRequestDTO updateAssignmentByTeacherRequest)
        {
            var exisitingDomainModel = await _dbContext.Assignments.FirstOrDefaultAsync(a => a.Id == assignmentId);

            if (exisitingDomainModel is null)
            {
                return null;
            }

            exisitingDomainModel.Title = updateAssignmentByTeacherRequest.AssignmentTitle;
            exisitingDomainModel.UploadLink = updateAssignmentByTeacherRequest.UploadLink;
            exisitingDomainModel.DueTime = updateAssignmentByTeacherRequest.DueTime;

            await _dbContext.SaveChangesAsync();
            return new AssignmentDTO
            {
                Id = exisitingDomainModel.Id,
                Title = exisitingDomainModel.Title,
                DueTime = exisitingDomainModel.DueTime,
                UploadLink = exisitingDomainModel.UploadLink,
                Status = exisitingDomainModel.Status,
                SubjectId = exisitingDomainModel.SubjectId
            };
        }

        public async Task<MaterialDTO?> UpdateMaterialForSubjectTopic(int materialId, UpdateMaterialRequestDTO updateMaterialRequest)
        {
            var existingDomainModel = await _dbContext.Materials.FirstOrDefaultAsync(m => m.Id == materialId);
            if (existingDomainModel is null)
            {
                return null;
            }
            existingDomainModel.SavedName = updateMaterialRequest.NewMaterialName;
            existingDomainModel.UploadLink = updateMaterialRequest.UploadLink;

            await _dbContext.SaveChangesAsync();
            return new MaterialDTO
            {
                Id = existingDomainModel.Id,
                SavedName = existingDomainModel.SavedName,
                UploadLink = existingDomainModel.UploadLink,
                TopicId = existingDomainModel.TopicId
            };
        }
    }
}
