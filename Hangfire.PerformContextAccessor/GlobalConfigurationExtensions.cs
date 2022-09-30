using Hangfire.Annotations;
using System;

namespace Hangfire.PerformContextAccessor
{
    public static class GlobalConfigurationExtensions
    {
        public static IGlobalConfiguration UsePerformContextAccessorFilter([NotNull] this IGlobalConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            return configuration.UseFilter<PerformContextAccessorFilter>(new PerformContextAccessorFilter(new PerformContextAccessor()));
        }
    }
}

