using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileStorage.Domain.Entities
{
    public class SubjectEntity
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public DateOnly CreatedDate { get; set; }

        [ForeignKey("TeacherEntity")]
        public int AssignedTeacherId { get; set; }
        public TeacherEntity TeacherEntity { get; set; }

        public ICollection<SubjectTopicEntity> SubjectTopicEntities { get; set; }
    }
}
