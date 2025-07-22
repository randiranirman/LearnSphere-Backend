using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnalyticsService.Domain.Entities
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int Grade { get; set; }

        [ForeignKey("Teacher")]
        public int AssignedTeacherId { get; set; }

        public Teacher AssignedTeacher { get; set; }

        public ICollection<Student> Students { get; set; }

        public ICollection<Assignment> Assignments { get; set; }

    }
}
