using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Utapau.Providers.Implementations;
using Utapau.Providers.Interfaces;

namespace Utapau.Providers
{
    /// <summary>
    /// Extension methods for adding services to an <see cref="IServiceCollection" />.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a provider for the type specified in <typeparamref name="TService"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddProvider<TService>(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IProvider<TService>, Provider<TService>>();
            
            return services;
        }
        
        /// <summary>
        /// Adds factory of the type specified in <typeparamref name="TService"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddFactory<TService>(this IServiceCollection services) where TService : class
        {
            var type = typeof(TService);
            if (services.All(s => s.ServiceType != type))
            {
                throw new InvalidOperationException($"No service for {type.FullName} has been registered");
            }
            
            services.AddSingleton<Func<TService>>(sp => sp.GetRequiredService<TService>);
            
            return services;
        }
    }
}