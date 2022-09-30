using Hangfire.Server;

namespace Hangfire.PerformContextAccessor
{
    public interface IPerformContextAccessor
    {
        PerformContext PerformingContext { get; set; }
    }
}