using System;
using Microsoft.Extensions.DependencyInjection;

namespace Utapau
{
    public static class ServiceProviderExtensions
    {
        public static TInterface GetRequiredService<TInterface>(
            this IServiceProvider serviceProvider, string dependencyName)
        {
            var dependencyType = DependencyDictionary.GetTypeByName<TInterface>(dependencyName);

            return (TInterface) serviceProvider.GetRequiredService(dependencyType);
        }
        
        public static TInterface GetService<TInterface>(
            this IServiceProvider serviceProvider, string dependencyName)
        {
            var dependencyType = DependencyDictionary.GetTypeByName<TInterface>(dependencyName);

            return (TInterface) serviceProvider.GetService(dependencyType);
        }
    }
}