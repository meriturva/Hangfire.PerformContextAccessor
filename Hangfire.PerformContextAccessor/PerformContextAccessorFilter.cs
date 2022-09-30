using Hangfire.Server;
using System;

namespace Hangfire.PerformContextAccessor
{
    public class PerformContextAccessorFilter : IServerFilter
    {
        private IPerformContextAccessor _performContextAccessor;

        public PerformContextAccessorFilter(IPerformContextAccessor performContextAccessor)
        {
            _performContextAccessor = performContextAccessor;
        }

        public void OnPerformed(PerformedContext context)
        {

        }

        public void OnPerforming(PerformingContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            _performContextAccessor.PerformingContext = context;
        }
    }
}
