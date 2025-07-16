using CourseRegistration.Domain.Models;

namespace CourseRegistration.Application.Dtos
{
    public class TeacherSubjectDto
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;
        public RegistrationStatus Status { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public int? ApprovedByAdminId { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
