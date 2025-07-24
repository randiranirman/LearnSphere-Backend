using Microsoft.Extensions.Configuration;
using NewAnalyticsService.Application.Interfaces;
using System.Text.Json;

namespace NewAnalyticsService.Infrastructure.Repositories
{
    public class AssignmentHttpService(IConfiguration configuration, HttpClient httpClient) : IAssignmentHttpService
    {
        public async Task<int> GetAssignmentCountBySubjectIdAsync(int subjectId)
        {
            try
            {
                var baseUrl = configuration["ExternalServices:FileStorage:BaseUrl"] ?? "https://localhost:7212";
                var url = $"{baseUrl}/api/Assignment/get-assignment-count-by-subjectId?subjectId={subjectId}";

                var response = await httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                    return 0;

                var json = await response.Content.ReadAsStringAsync();
                var count = JsonSerializer.Deserialize<int>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return count;
            }
            catch
            {
                // Optionally log the error here
                return 0;
            }
        }
    }
}
