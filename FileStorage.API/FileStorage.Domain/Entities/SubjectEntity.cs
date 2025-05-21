namespace FileStorage.Domain.Entities
{
    public class SubjectEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateOnly CreatedDate { get; set; }

        public int AssignedTeacherId { get; set; }
        public TeacherEntity AssignedTeacher { get; set; }

        public ICollection<SubjectTopicEntity> SubjectTopics { get; set; }
    }
}
