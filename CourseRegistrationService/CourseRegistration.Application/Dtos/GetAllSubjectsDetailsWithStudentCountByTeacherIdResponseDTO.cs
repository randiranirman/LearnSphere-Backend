namespace CourseRegistration.Application.Dtos
{
    public class GetAllSubjectsDetailsWithStudentCountByTeacherIdResponseDTO
    {

        public int SubjectId { get; set; }
        public string SubjectTitle { get; set; }
        public string Code { get; set; }
        public int NoOfRegisteredStudents { get; set; }
    }
}
