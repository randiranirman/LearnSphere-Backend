using System.Text.Json;
using CourseRegistration.Application.Interfaces;
using CourseRegistration.Application.Dtos;
using Microsoft.Extensions.Configuration;

namespace CourseRegistration.Application.Services
{
    public class StudentHttpService : IStudentHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public StudentHttpService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<StudentDto?> GetStudentByIdAsync(int studentId)
        {
            try
            {
                var userServiceUrl = _configuration["ExternalServices:UserManagement:BaseUrl"] ?? "https://localhost:7033";
                var endpoint = $"{userServiceUrl}/user/students/{studentId}";
                
                var response = await _httpClient.GetAsync(endpoint);
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonContent = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    
                    return JsonSerializer.Deserialize<StudentDto>(jsonContent, options);
                }
                
                return null;
            }
            catch (Exception ex)
            {
                // Log the exception (you can inject ILogger here)
                throw new InvalidOperationException($"Error retrieving student with ID {studentId}: {ex.Message}", ex);
            }
        }

        public async Task<bool> ValidateStudentExistsAsync(int studentId)
        {
            var student = await GetStudentByIdAsync(studentId);
            return student != null;
        }

        public async Task<List<StudentDto>> GetStudentsByIdsAsync(List<int> studentIds)
        {
            var students = new List<StudentDto>();
            
            foreach (var studentId in studentIds)
            {
                var student = await GetStudentByIdAsync(studentId);
                if (student != null)
                {
                    students.Add(student);
                }
            }
            
            return students;
        }
    }
}
