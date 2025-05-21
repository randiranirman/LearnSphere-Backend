namespace FileStorage.Domain.Entities
{
    public class SubjectTopicEntity
    {
        public int Id { get; set; }
        public string? TopicName { get; set; }

        public int SubjectId { get; set; }
        public SubjectEntity Subject { get; set; }

        public ICollection<MetirialEntity> Metirials { get; set; }
    }
}
