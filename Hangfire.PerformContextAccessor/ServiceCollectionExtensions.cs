using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Hangfire.PerformContextAccessor
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a default implementation for the <see cref="IPerformContextAccessor"/> service.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddHangfirePerformContextAccessor(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddSingleton<IPerformContextAccessor, PerformContextAccessor>();
            return services;
        }
    }
}
