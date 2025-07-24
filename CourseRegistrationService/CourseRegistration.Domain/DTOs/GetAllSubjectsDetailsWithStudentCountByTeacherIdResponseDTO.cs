namespace CourseRegistration.Domain.DTOs;

public class GetAllSubjectsDetailsWithStudentCountByTeacherIdResponseDTO
{
    public int SubjectId { get; set; }
    public string? SubjectTitle { get; set; }
    public int StudentCount { get; set; }
}
