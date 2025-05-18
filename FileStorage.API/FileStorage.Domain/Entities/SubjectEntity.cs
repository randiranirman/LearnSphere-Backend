namespace FileStorage.Domain.Entities
{
    public class SubjectEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateOnly CreatedDate { get; set; }

        // foreign key
        public int AssignedTeacherId { get; set; }

        // navigation prop
        public TeacherEntity AssignedTeacher { get; set; }

        public ICollection<SubjectTopicEntity> SubjectTopics { get; set; }
    }
}
