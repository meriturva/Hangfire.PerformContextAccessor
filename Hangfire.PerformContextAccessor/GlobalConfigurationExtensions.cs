using Hangfire.Annotations;
using Microsoft.Extensions.DependencyInjection;
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

        public static IGlobalConfiguration UsePerformContextAccessorFilter([NotNull] this IGlobalConfiguration configuration, IServiceProvider serviceProvider)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            IPerformContextAccessor performContextAccessor = serviceProvider.GetService<IPerformContextAccessor>();
            return configuration.UseFilter<PerformContextAccessorFilter>(new PerformContextAccessorFilter(performContextAccessor));
        }
    }
}

