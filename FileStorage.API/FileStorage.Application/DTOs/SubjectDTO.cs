namespace FileStorage.Application.DTOs
{
    public class SubjectDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateOnly CreatedDate { get; set; }
        public int AssignedTeacherId { get; set; }
        public List<SubjectTopicDTO> Topics { get; set; } = new();
    }
}
