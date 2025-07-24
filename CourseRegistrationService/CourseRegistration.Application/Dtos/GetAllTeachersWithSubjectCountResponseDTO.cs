namespace CourseRegistration.Application.Dtos
{
    public class GetAllTeachersWithSubjectCountResponseDTO
    {
        public int TeacherId { get; set; }
        public string TeacherFullName { get; set; } = string.Empty;
        public string EmployeeId { get; set; }
        public string TeacherEmail { get; set; } = string.Empty;
        public int SubjectCount { get; set; }
    }
}
