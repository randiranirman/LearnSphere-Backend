namespace FileStorage.Application.DTOs
{
    public class AssignmentDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DueTime { get; set; }
        public string UploadLink { get; set; }
        public string Status { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
    }
}
