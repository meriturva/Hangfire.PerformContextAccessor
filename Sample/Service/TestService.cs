using Hangfire.PerformContextAccessor;

namespace Sample.Service
{
    public class TestService : ITestService
    {
        private IPerformContextAccessor _performContextAccessor;

        public TestService(IPerformContextAccessor performContextAccessor)
        {
            _performContextAccessor = performContextAccessor;
        }

        public string GetCurrentJobId()
        {
            return _performContextAccessor.PerformingContext.BackgroundJob.Id;
        }

        public async Task<string> GetCurrentJobIdAsync()
        {
            var jobId = _performContextAccessor.PerformingContext.BackgroundJob.Id;
            return await Task.FromResult(jobId);
        }
    }
}
