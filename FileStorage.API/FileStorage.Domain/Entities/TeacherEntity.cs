namespace FileStorage.Domain.Entities
{
    public class TeacherEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateOnly BirthDay { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }

        // navigation property
        public ICollection<SubjectEntity> Subjects { get; set; }  // to confirms the one-to-many relation

    }
}
