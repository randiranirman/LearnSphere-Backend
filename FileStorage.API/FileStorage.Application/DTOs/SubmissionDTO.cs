namespace FileStorage.Application.DTOs
{
    public class SubmissionDTO
    {
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public int StudentId { get; set; }
        public DateTime AssignmentDueTime { get; set; }
        public string Status { get; set; }
        public string UploadLink { get; set; } // this stores the link to the file storage which store the submission file
        public string SubmissionName { get; set; }
        public DateTime SubmitedTime { get; set; }

    }
}
