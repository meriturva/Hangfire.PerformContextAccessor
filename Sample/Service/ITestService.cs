using System.Threading.Tasks;

namespace Sample.Service
{
    public interface ITestService
    {
        string GetCurrentJobId();
        Task<string> GetCurrentJobIdAsync();
    }
}