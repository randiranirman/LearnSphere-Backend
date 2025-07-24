using Microsoft.Extensions.Configuration;
using NewAnalyticsServcie.Application.DTOs;
using NewAnalyticsServcie.Application.Interfaces;
using System.Net.Http;
using System.Text.Json;

namespace NewAnalyticsService.Infrastructure.Repositories
{
    public class TeacherSubjectHttpService(IConfiguration configuration, HttpClient httpClient) : ITeacherSubjectHttpService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly HttpClient _httpClient = httpClient;

        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<List<SubjectsByTeacherIdDTO>> GetSubjectsByTeacherIdAsync(int teacherId)
        {
            try
            {
                var baseUrl = _configuration["ExternalServices:CourseRegistration:BaseUrl"] ?? "https://localhost:7293";
                var url = $"{baseUrl}/subjects/Subject/getSubjectsByTeacherId/{teacherId}";

                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                    return new List<SubjectsByTeacherIdDTO>();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<SubjectsByTeacherIdDTO>>(json, _jsonOptions) ?? new List<SubjectsByTeacherIdDTO>();
            }
            catch (Exception ex)
            {
                // Optional: log the exception
                return new List<SubjectsByTeacherIdDTO>();
            }
        }

        public async Task<int> GetRegisteredStudentCountBySubjectIdAsync(int subjectId)
        {
            try
            {
                var baseUrl = _configuration["ExternalServices:CourseRegistration:BaseUrl"] ?? "https://localhost:7293";
                var url = $"{baseUrl}/registrations/students/Student/student/get-student-count-by-subjectId?subjectId={subjectId}";

                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    // Optional: log response failure
                    return 0;
                }

                var json = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                // Assuming the API returns a plain integer (e.g., 5)
                var studentCount = JsonSerializer.Deserialize<int>(json, options);
                return studentCount;
            }
            catch (Exception ex)
            {
                // Optional: log the exception
                return 0;
            }
        }

    }
}
