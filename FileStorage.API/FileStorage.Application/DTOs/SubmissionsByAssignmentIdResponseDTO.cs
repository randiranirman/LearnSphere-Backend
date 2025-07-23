namespace FileStorage.Application.DTOs
{
    public class SubmissionsByAssignmentIdResponseDTO
    {
        public int SubmissionId { get; set; }
        public int StudentId { get; set; }
        public string SubmissionName { get; set; }
        public string SubmissionStatus { get; set; }
        public string UploadLink { get; set; }

    }
}
