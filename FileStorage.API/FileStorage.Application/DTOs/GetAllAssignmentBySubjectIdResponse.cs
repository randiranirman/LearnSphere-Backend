namespace FileStorage.Application.DTOs
{
    public class GetAllAssignmentBySubjectIdResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public int NoOfSubmissions { get; set; }
    }
}
