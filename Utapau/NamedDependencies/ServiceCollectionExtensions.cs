using System;
using Microsoft.Extensions.DependencyInjection;

namespace Utapau.NamedDependencies
{
    /// <summary>
    /// Extension methods for adding services to an <see cref="IServiceCollection" />.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TInterface"/> with an
        /// implementation type specified in <typeparamref name="TImplementation" /> using the
        /// dependency name specified in <paramref name="dependencyName"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TInterface">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="dependencyName">Dependency name.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public static IServiceCollection AddSingleton<TInterface, TImplementation>(
            this IServiceCollection services, string dependencyName)
            where TImplementation : class, TInterface
        {
            DependencyDictionary.Register<TInterface, TImplementation>(dependencyName);
            services.AddSingleton<TImplementation>();
            
            return services;
        }
        
        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TInterface"/> with an
        /// implementation type specified in <typeparamref name="TImplementation" /> using the
        /// dependency name specified in <paramref name="dependencyName"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TInterface">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <param name="dependencyName">Dependency name.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public static IServiceCollection AddSingleton<TInterface, TImplementation>(
            this IServiceCollection services,
            Func<IServiceProvider, TImplementation> implementationFactory,
            string dependencyName)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            DependencyDictionary.Register<TInterface, TImplementation>(dependencyName);
            services.AddSingleton(implementationFactory);
            
            return services;
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService"/>  using the
        /// dependency name specified in <paramref name="dependencyName"/> to the
        /// specified <see cref="IServiceCollection"/>
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="dependencyName">Dependency name.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public static IServiceCollection AddSingleton<TService>(
            this IServiceCollection services, string dependencyName)
            where TService : class
        {
            DependencyDictionary.Register<TService, TService>(dependencyName);
            services.AddSingleton<TService>();
            
            return services;
        }
        
        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService"/>  using the
        /// dependency name specified in <paramref name="dependencyName"/> to the
        /// specified <see cref="IServiceCollection"/>
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="dependencyName">Dependency name.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public static IServiceCollection AddSingleton<TService>(
            this IServiceCollection services, 
            Func<IServiceProvider, TService> implementationFactory,
            string dependencyName)
            where TService : class
        {
            DependencyDictionary.Register<TService, TService>(dependencyName);
            services.AddSingleton(implementationFactory);
            
            return services;
        }
        
        /// <summary>
        /// Adds a scoped service of the type specified in <typeparamref name="TInterface"/> with an
        /// implementation type specified in <typeparamref name="TImplementation" /> using the
        /// dependency name specified in <paramref name="dependencyName"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TInterface">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="dependencyName">Dependency name.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Scoped"/>
        public static IServiceCollection AddScoped<TInterface, TImplementation>(
            this IServiceCollection services, string dependencyName)
            where TImplementation : class, TInterface
        {
            DependencyDictionary.Register<TInterface, TImplementation>(dependencyName);
            services.AddScoped<TImplementation>();
            
            return services;
        }
        
        /// <summary>
        /// Adds a scoped service of the type specified in <typeparamref name="TInterface"/> with an
        /// implementation type specified in <typeparamref name="TImplementation" /> using the
        /// dependency name specified in <paramref name="dependencyName"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TInterface">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <param name="dependencyName">Dependency name.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Scoped"/>
        public static IServiceCollection AddScoped<TInterface, TImplementation>(
            this IServiceCollection services,
            Func<IServiceProvider, TImplementation> implementationFactory,
            string dependencyName)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            DependencyDictionary.Register<TInterface, TImplementation>(dependencyName);
            services.AddScoped(implementationFactory);
            
            return services;
        }
        
        /// <summary>
        /// Adds a scoped service of the type specified in <typeparamref name="TService"/>  using the
        /// dependency name specified in <paramref name="dependencyName"/> to the
        /// specified <see cref="IServiceCollection"/>
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="dependencyName">Dependency name.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Scoped"/>
        public static IServiceCollection AddScoped<TService>(
            this IServiceCollection services, string dependencyName)
            where TService : class
        {
            DependencyDictionary.Register<TService, TService>(dependencyName);
            services.AddScoped<TService>();
            
            return services;
        }
        
        /// <summary>
        /// Adds a scoped service of the type specified in <typeparamref name="TService"/>  using the
        /// dependency name specified in <paramref name="dependencyName"/> to the
        /// specified <see cref="IServiceCollection"/>
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="dependencyName">Dependency name.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Scoped"/>
        public static IServiceCollection AddScoped<TService>(
            this IServiceCollection services, 
            Func<IServiceProvider, TService> implementationFactory,
            string dependencyName)
            where TService : class
        {
            DependencyDictionary.Register<TService, TService>(dependencyName);
            services.AddScoped(implementationFactory);
            
            return services;
        }
        
        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TInterface"/> with an
        /// implementation type specified in <typeparamref name="TImplementation" /> using the
        /// dependency name specified in <paramref name="dependencyName"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TInterface">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="dependencyName">Dependency name.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
        public static IServiceCollection AddTransient<TInterface, TImplementation>(
            this IServiceCollection services, string dependencyName) 
            where TImplementation : class, TInterface
        {
            DependencyDictionary.Register<TInterface, TImplementation>(dependencyName);
            services.AddTransient<TImplementation>();
            
            return services;
        }
        
        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TInterface"/> with an
        /// implementation type specified in <typeparamref name="TImplementation" /> using the
        /// dependency name specified in <paramref name="dependencyName"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TInterface">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <param name="dependencyName">Dependency name.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
        public static IServiceCollection AddTransient<TInterface, TImplementation>(
            this IServiceCollection services,
            Func<IServiceProvider, TImplementation> implementationFactory,
            string dependencyName)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            DependencyDictionary.Register<TInterface, TImplementation>(dependencyName);
            services.AddTransient(implementationFactory);
            
            return services;
        }
        
        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TService"/>  using the
        /// dependency name specified in <paramref name="dependencyName"/> to the
        /// specified <see cref="IServiceCollection"/>
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="dependencyName">Dependency name.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
        public static IServiceCollection AddTransient<TService>(
            this IServiceCollection services, string dependencyName)
            where TService : class
        {
            DependencyDictionary.Register<TService, TService>(dependencyName);
            services.AddTransient<TService>();
            
            return services;
        }
        
        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TService"/>  using the
        /// dependency name specified in <paramref name="dependencyName"/> to the
        /// specified <see cref="IServiceCollection"/>
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="dependencyName">Dependency name.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
        public static IServiceCollection AddTransient<TService>(
            this IServiceCollection services, 
            Func<IServiceProvider, TService> implementationFactory,
            string dependencyName)
            where TService : class
        {
            DependencyDictionary.Register<TService, TService>(dependencyName);
            services.AddTransient(implementationFactory);
            
            return services;
        }

        /// <summary>
        /// Clears list of all named registrations in the specified <see cref="IServiceCollection"/>
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to clear named registrations from.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection ClearNamedRegistrations(this IServiceCollection services)
        {
            DependencyDictionary.Clear();
            
            return services;
        }
    }
}