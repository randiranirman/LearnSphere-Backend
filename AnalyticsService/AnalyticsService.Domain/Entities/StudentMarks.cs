using System.ComponentModel.DataAnnotations.Schema;

namespace AnalyticsService.Domain.Entities
{
    public class StudentMarks
    {
        [ForeignKey("Assignment")]
        public int AssignmentId { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public int Marks { get; set; } // this is the field for store marks => 'A', 'B', 'C', 'S', 'F'

        public Assignment Assignment { get; set; }
        public Student Student { get; set; }
    }
}
