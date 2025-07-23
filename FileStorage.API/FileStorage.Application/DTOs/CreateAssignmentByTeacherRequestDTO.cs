namespace FileStorage.Application.DTOs
{
    public class CreateAssignmentByTeacherRequestDTO
    {
        public string AssignmentTitle { get; set; }
        public DateTime DueTime { get; set; }
        public string UploadLink { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
    }
}
