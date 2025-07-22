namespace FileStorage.Application.DTOs
{
    public class SubjectsDTO
    {
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        
        // Optional: Keep the original property for backwards compatibility
        public string SubjectTitle => Name;
    }
}
