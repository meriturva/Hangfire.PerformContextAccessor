using Microsoft.Extensions.Logging;
using Sample.Service;
using System.Threading.Tasks;

namespace Sample.Jobs
{
    public class SimpleJob
    {
        private ILogger<SimpleJob> _logger;
        private ITestService _testService;

        public SimpleJob(ILogger<SimpleJob> logger, ITestService testService)
        {
            _logger = logger;
            _testService = testService;
        }

        public async Task DoJobAsync()
        {
            _logger.LogInformation("Test log message");

            _logger.LogInformation("Current JobId from service sync {jobId}", _testService.GetCurrentJobId());

            var jobId = await _testService.GetCurrentJobIdAsync();
            _logger.LogInformation("Current JobId from service async {jobId}", jobId);

            await Task.CompletedTask;
        }
    }
}
