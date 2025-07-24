using NewAnalyticsService.Application.DTOs;
using NewAnalyticsService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace NewAnalyticsService.Infrastructure.Repositories
{
    public class StudentDetailsHttpRepository(HttpClient httpClient) : IStudentDetailsHttpRepository
    {
        public async Task<IEnumerable<StudentDetailsDTO>> GetAllStudentsDetailsBySubjectId(int subjectId)
        {
            try
            {
                var endpoint = $"https://localhost:7293/registrations/students/Student/students/student-details-by-subjectId?subjectId={subjectId}";

                var response = await httpClient.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var students = await response.Content.ReadFromJsonAsync<IEnumerable<StudentDetailsDTO>>();
                    return students ?? new List<StudentDetailsDTO>();
                }
                else
                {
                    // Optional: Log the error
                    throw new Exception($"Request failed with status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Optional: Log the exception
                throw new ApplicationException("Failed to retrieve student details by subject ID.", ex);
            }
        }
    }
}
