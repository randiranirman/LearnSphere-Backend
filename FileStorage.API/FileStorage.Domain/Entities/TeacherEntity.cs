using System.ComponentModel.DataAnnotations;

namespace FileStorage.Domain.Entities
{
    public class TeacherEntity
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateOnly BirthDay { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }

        public ICollection<SubjectEntity> SubjectEntities { get; set; }

    }
}
