using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace FileStorage.Application.Services
{
    public class CourseHttpService : ICourseHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CourseHttpService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<IEnumerable<SubjectsDTO>> GetSubjectsByTeacherIdAsync(int teacherId)
        {
            try
            {
                var baseUrl = _configuration["ExternalServices:CourseRegistration:BaseURL"];
                if (string.IsNullOrEmpty(baseUrl))
                {
                    throw new InvalidOperationException("ExternalServices:CourseRegistration:BaseURL configuration is missing");
                }
                
                var endpoint = $"{baseUrl}/subjects/Subject/getSubjectsByTeacherId/{teacherId}";

                var response = await _httpClient.GetAsync(endpoint);
                if (!response.IsSuccessStatusCode)
                {
                    // Log the error status code and reason
                    Console.WriteLine($"HTTP Error: {response.StatusCode} - {response.ReasonPhrase} for endpoint: {endpoint}");
                    return new List<SubjectsDTO>();
                }

                var json = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(json))
                {
                    Console.WriteLine($"Empty response from endpoint: {endpoint}");
                    return new List<SubjectsDTO>();
                }
                
                Console.WriteLine($"Raw JSON response: {json}");
                
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<List<SubjectsDTO>>(json, option);
                Console.WriteLine($"Successfully deserialized {result?.Count ?? 0} subjects");
                return result ?? new List<SubjectsDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetSubjectsByTeacherIdAsync for teacherId {teacherId}: {ex.Message}");
                return new List<SubjectsDTO>();
            }
        }
    }
}
