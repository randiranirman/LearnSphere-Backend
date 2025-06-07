namespace FileStorage.Application.DTOs
{
    public class SubjectDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateOnly CreatedDate { get; set; }
        public List<TopicDTO> Topics { get; set; }
    }
}
