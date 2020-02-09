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
    }
}