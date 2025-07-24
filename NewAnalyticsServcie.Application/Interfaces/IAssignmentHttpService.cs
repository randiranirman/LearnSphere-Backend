namespace NewAnalyticsServcie.Application.Interfaces
{
    public interface IAssignmentHttpService
    {
        public Task<int> GetAssignmentCountBySubjectIdAsync(int subjectId);

    }
}
