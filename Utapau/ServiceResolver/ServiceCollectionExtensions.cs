using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Utapau.ServiceResolver
{
    /// <summary>
    /// Extension methods for adding services to an <see cref="IServiceCollection" />.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Resolves all registered services. Throws an exception if service resolving fails.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to resolve services from.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection ResolveAllServices(this IServiceCollection services)
        {
            return services.ResolveAllServicesWhere(t => true);
        }
        
        /// <summary>
        /// Resolves all filtered services. Throws an exception if service resolving fails.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to resolve services from.</param>
        /// <param name="predicate">Services filter</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection ResolveAllServicesWhere(this IServiceCollection services,
            Func<Type, bool> predicate)
        {
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var types = services
                    .Select(s => s.ServiceType)
                    .Where(t => !t.IsAbstract && predicate(t));

                foreach (var type in types)
                {
                    serviceProvider.GetRequiredService(type);
                }
            }
            
            return services;
        }
    }
}