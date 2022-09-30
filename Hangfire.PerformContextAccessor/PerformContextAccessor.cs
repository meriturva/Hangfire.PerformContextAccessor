using Hangfire.Server;
using System.Threading;

namespace Hangfire.PerformContextAccessor
{
    public class PerformContextAccessor : IPerformContextAccessor
    {
        private static AsyncLocal<PerformContextHolder> _performContextCurrent = new AsyncLocal<PerformContextHolder>();

        public PerformContext PerformingContext
        {
            get
            {
                return _performContextCurrent.Value?.Context;
            }
            set
            {
                var holder = _performContextCurrent.Value;
                if (holder != null)
                {
                    // Clear current PerformContext trapped in the AsyncLocals, as its done.
                    holder.Context = null;
                }

                if (value != null)
                {
                    // Use an object indirection to hold the PerformContext in the AsyncLocal,
                    // so it can be cleared in all ExecutionContexts when its cleared.
                    _performContextCurrent.Value = new PerformContextHolder { Context = value };
                }
            }
        }

        private class PerformContextHolder
        {
            public PerformContext Context;
        }
    }
}
