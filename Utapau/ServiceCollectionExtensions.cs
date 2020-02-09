using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Utapau
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSingleton<TInterface, TImplementation>(
            this IServiceCollection services, string dependencyName)  where TImplementation : class
        {
            DependencyDictionary.Register<TInterface, TImplementation>(dependencyName);
            services.AddSingleton<TImplementation>();
            
            return services;
        }
        
        public static IServiceCollection AddScoped<TInterface, TImplementation>(
            this IServiceCollection services, string dependencyName)  where TImplementation : class
        {
            DependencyDictionary.Register<TInterface, TImplementation>(dependencyName);
            services.AddScoped<TImplementation>();
            
            return services;
        }
        
        public static IServiceCollection AddTransient<TInterface, TImplementation>(
            this IServiceCollection services, string dependencyName)  where TImplementation : class
        {
            DependencyDictionary.Register<TInterface, TImplementation>(dependencyName);
            services.AddTransient<TImplementation>();
            
            return services;
        }

        public static IServiceCollection AddFactory<T>(this IServiceCollection services) where T : class
        {
            var type = typeof(T);
            if (services.All(s => s.ServiceType != type))
            {
                throw new InvalidOperationException($"No service for {type.FullName} has been registered");
            }
            
            services.AddSingleton<Func<T>>(sp => sp.GetRequiredService<T>);
            
            return services;
        }

        public static IServiceCollection ResolveAllServices(this IServiceCollection services)
        {
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var types = services
                    .Select(s => s.ServiceType)
                    .Where(t => !t.IsAbstract);

                foreach (var type in types)
                {
                    serviceProvider.GetRequiredService(type);
                }
            }
            
            return services;
        }
    }
}