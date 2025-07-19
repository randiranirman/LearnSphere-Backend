using FileStorage.Application.DTOs;

namespace FileStorage.Application.Interfaces
{
    public interface ITeacherFilesRepository
    {
        public Task<IEnumerable<SubjectsDTO?>> GetAllSubjectsByTeacherId(int teacherId);

        public Task<IEnumerable<AssignmentDTO>> GetAllAssignmentsBySubjectId(int subjectId);

        public Task<IEnumerable<SubjectMateriealsResponseDTO>> GetSubjectMateriealsBySubjectId(int subjectId);

        public Task<IEnumerable<SubmissionsByAssignmentIdResponseDTO>> GetAllSubmissionsByAssignmentId(int assignmentId);

        public Task<SubjectTopicDTO> CreateNewSubjectTopic(int subjectId, string newSubjectTopic);

        public Task<CreateMaterialResponseDTO> CreateMaterialForTopics(int topicId, CreateMaterialRequestDTO createMaterialRequest);

        public Task<SubjectTopicDTO?> EditSubjectTopic(int topicId, string newTopicName);

        public Task<MaterialDTO?> DeleteMaterialByMaterialId(int materialId);

        public Task<AssignmentDTO> CreateAssignmentByTeacher(CreateAssignmentByTeacherRequestDTO createAssignmentByTeacherRequest);

        public Task<AssignmentDTO?> UpdateAssignmentByTeacher(int assignmentId, UpdateAssignmentByTeacherRequestDTO updateAssignmentByTeacherRequest);

        public Task<AssignmentDTO> DeleteAssignmentByTeacher(int assignmentId);
    }
}
