using System;
using Microsoft.Extensions.DependencyInjection;

namespace Utapau.NamedDependencies
{
    /// <summary>
    /// Extension methods for adding services to an <see cref="IServiceProvider" />.
    /// </summary>
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// Get service of type <typeparamref name="T"/> from the <see cref="IServiceProvider"/>.
        /// </summary>
        /// <typeparam name="TInterface">The type of service object to get.</typeparam>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/> to retrieve the service object from.</param>
        /// <param name="dependencyName">Dependency name</param>
        /// <returns>A service object of type <typeparamref name="TInterface"/>.</returns>
        public static TInterface GetRequiredService<TInterface>(
            this IServiceProvider serviceProvider, string dependencyName)
        {
            var dependencyType = DependencyDictionary.GetTypeByName<TInterface>(dependencyName);

            return (TInterface) serviceProvider.GetRequiredService(dependencyType);
        }
        
        /// <summary>
        /// Get service of type <typeparamref name="T"/> from the <see cref="IServiceProvider"/>.
        /// </summary>
        /// <typeparam name="TInterface">The type of service object to get.</typeparam>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/> to retrieve the service object from.</param>
        /// <param name="dependencyName">Dependency name</param>
        /// <returns>A service object of type <typeparamref name="TInterface"/>.</returns>
        public static TInterface GetService<TInterface>(
            this IServiceProvider serviceProvider, string dependencyName)
        {
            var dependencyType = DependencyDictionary.GetTypeByName<TInterface>(dependencyName);

            return (TInterface) serviceProvider.GetService(dependencyType);
        }
    }
}