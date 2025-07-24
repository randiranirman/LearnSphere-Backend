using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CourseRegistration.Application.Dtos;
using CourseRegistration.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CourseRegistration.Application.Services
{
    public class TeacherHttpService : ITeacherHttpService
    {

         private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public TeacherHttpService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<GetAllTeachersDTO>> GetAllTeachersAsync()
        {
            try
            {
                var userServiceUrl = _configuration["ExternalServices:UserManagement:BaseUrl"] ?? "https://localhost:7033";
                var endpoint = $"{userServiceUrl}/user/teachers/get-all-teachers";

                var response = await _httpClient.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var jsonContent = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var teachers = JsonSerializer.Deserialize<List<GetAllTeachersDTO>>(jsonContent, options);
                    return teachers ?? new List<GetAllTeachersDTO>();
                }

                return new List<GetAllTeachersDTO>();
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                throw new InvalidOperationException($"Error retrieving all teachers: {ex.Message}", ex);
            }
        }


        public async Task<TeacherDto?> GetTeacherByIdAsync(int teachedID)
        {
            try
            {
                var userServiceUrl = _configuration["ExternalServices:UserManagement:BaseUrl"] ?? "https://localhost:7033";
                var endpoint = $"{userServiceUrl}/user/teachers/{teachedID}";

                var response = await _httpClient.GetAsync(endpoint);


                if (response.IsSuccessStatusCode)
                {
                    var jsonContent = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    return JsonSerializer.Deserialize<TeacherDto>(jsonContent, options);
                }

                return null;


            }
            catch (Exception ex)
            {
                // Log the exception (you can inject ILogger here)
                throw new InvalidOperationException($"Error retrieving teacher with ID {teachedID}: {ex.Message}", ex);
            }


        }

        public Task<bool> ValidateStudentExistsAsync(int studentId)
        {
            throw new NotImplementedException();
        }
    }
}
