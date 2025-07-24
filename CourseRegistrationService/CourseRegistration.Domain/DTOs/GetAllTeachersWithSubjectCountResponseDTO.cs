namespace CourseRegistration.Domain.DTOs;

public class GetAllTeachersWithSubjectCountResponseDTO
{
    public int TeacherId { get; set; }
    public string? TeacherFullName { get; set; }
    public string? TeacherEmail { get; set; }
    public int SubjectCount { get; set; }
}
