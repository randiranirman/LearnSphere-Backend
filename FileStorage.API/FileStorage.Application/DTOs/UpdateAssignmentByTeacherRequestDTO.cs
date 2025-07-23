namespace FileStorage.Application.DTOs
{
    public class UpdateAssignmentByTeacherRequestDTO
    {
        public string AssignmentTitle { get; set; }
        public DateTime DueTime { get; set; }
        public string UploadLink { get; set; }
    }
}
