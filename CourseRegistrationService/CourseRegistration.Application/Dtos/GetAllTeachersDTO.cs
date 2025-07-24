namespace CourseRegistration.Application.Dtos
{
    public class GetAllTeachersDTO
    {
        public int TeacherID { get; set; }
        public string TeacherName { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string EmployeeId { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
