using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Utapau.NamedDependencies;
using Utapau.Tests.Services;

namespace Utapau.Tests
{
    public class NamedRegistrationsTests : TestsBase
    {
        private const string FirstServiceDependencyName = nameof(FirstService);
        private const string SecondServiceDependencyName = nameof(SecondService);

        [Test]
        public void TestSingleton()
        {
            Services
                .AddSingleton<IService, FirstService>(FirstServiceDependencyName)
                .AddSingleton<IService, SecondService>(SecondServiceDependencyName);

            VerifyServices();
        }

        [Test]
        public void TestSingletonWithoutInterface()
        {
            Services
                .AddSingleton<FirstService>(FirstServiceDependencyName)
                .AddSingleton<SecondService>(SecondServiceDependencyName);

            VerifyExplicitlyRegisteredServices();
        }
        
        [Test]
        public void TestSingletonWithFactory()
        {
            Services
                .AddSingleton<IService, FirstService>(sp =>
                    new FirstService(), FirstServiceDependencyName)
                .AddSingleton<IService, SecondService>(sp => new SecondService(),
            SecondServiceDependencyName);

            VerifyServices();
        }
        
        [Test]
        public void TestSingletonWithFactoryWithoutInterface()
        {
            Services
                .AddSingleton(sp => new FirstService(), FirstServiceDependencyName)
                .AddSingleton(sp => new SecondService(), SecondServiceDependencyName);

            VerifyExplicitlyRegisteredServices();
        }

        [Test]
        public void TestScoped()
        {
            Services
                .AddScoped<IService, FirstService>(FirstServiceDependencyName)
                .AddScoped<IService, SecondService>(SecondServiceDependencyName);

            VerifyServices();
        }
        
        [Test]
        public void TestScopedWithoutInterface()
        {
            Services
                .AddScoped<FirstService>(FirstServiceDependencyName)
                .AddScoped<SecondService>(SecondServiceDependencyName);

            VerifyExplicitlyRegisteredServices();
        }
        
        [Test]
        public void TestScopedWithFactory()
        {
            Services
                .AddScoped<IService, FirstService>(sp =>
                    new FirstService(), FirstServiceDependencyName)
                .AddScoped<IService, SecondService>(sp => new SecondService(),
                    SecondServiceDependencyName);

            VerifyServices();
        }
        
        [Test]
        public void TestAddScopedWithFactoryWithoutInterface()
        {
            Services
                .AddScoped(sp => new FirstService(), FirstServiceDependencyName)
                .AddScoped(sp => new SecondService(), SecondServiceDependencyName);

            VerifyExplicitlyRegisteredServices();
        }

        [Test]
        public void TestTransient()
        {
            Services
                .AddTransient<IService, FirstService>(FirstServiceDependencyName)
                .AddTransient<IService, SecondService>(SecondServiceDependencyName);

            VerifyServices();
        }
        
        [Test]
        public void TestTransientWithoutInterface()
        {
            Services
                .AddTransient<FirstService>(FirstServiceDependencyName)
                .AddTransient<SecondService>(SecondServiceDependencyName);

            VerifyExplicitlyRegisteredServices();
        }
        
        [Test]
        public void TestTransientWithFactory()
        {
            Services
                .AddTransient<IService, FirstService>(sp =>
                    new FirstService(), FirstServiceDependencyName)
                .AddTransient<IService, SecondService>(sp => new SecondService(),
                    SecondServiceDependencyName);

            VerifyServices();
        }
        
        [Test]
        public void TestTransientWithFactoryWithoutInterface()
        {
            Services
                .AddTransient(sp => new FirstService(), FirstServiceDependencyName)
                .AddTransient(sp => new SecondService(), SecondServiceDependencyName);

            VerifyExplicitlyRegisteredServices();
        }
        
        [Test]
        public void TestServiceNotFoundException()
        {
            using var serviceProvider = BuildServiceProvider();
            void GetService() => serviceProvider.GetRequiredService<IService>(FirstServiceDependencyName);
            
            Assert.Throws<KeyNotFoundException>(GetService);
        }
        
        [Test]
        public void TestKeyForServiceNotFoundException()
        {
            Services
                .AddSingleton<IService, FirstService>(FirstServiceDependencyName);
            
            using var serviceProvider = BuildServiceProvider();
            void GetService() => serviceProvider.GetRequiredService<IService>(SecondServiceDependencyName);
            
            Assert.Throws<KeyNotFoundException>(GetService);
        }
        
        [Test]
        public void TestFactory()
        {
            Services
                .AddSingleton<IService, FirstService>(FirstServiceDependencyName)
                .AddSingleton<IService, SecondService>(SecondServiceDependencyName)
                .AddSingleton(sp => new ForthService(
                    sp.GetRequiredService<IService>(FirstServiceDependencyName),
                    sp.GetRequiredService<IService>(SecondServiceDependencyName)
                ));
            
            using var serviceProvider = BuildServiceProvider();
            var forthService = serviceProvider.GetRequiredService<ForthService>();
            Assert.True(forthService.FirstService is FirstService);
            Assert.True(forthService.SecondService is SecondService);
        }
        
        [Test]
        public void TestDoubleRegistrationException()
        {
            void RegisterServices() =>
                Services
                    .AddSingleton<IService, FirstService>(FirstServiceDependencyName)
                    .AddSingleton<IService, SecondService>(FirstServiceDependencyName);
            
            Assert.Throws<InvalidOperationException>(RegisterServices);
        }
        
        [Test]
        public void TestFactoryDoubleRegistrationException()
        {
            void RegisterServices() =>
                Services
                    .AddSingleton<IService, FirstService>(FirstServiceDependencyName)
                    .AddSingleton<IService, FirstService>(sp => new FirstService(),
                        FirstServiceDependencyName);
            
            Assert.Throws<InvalidOperationException>(RegisterServices);
        }
        
        [Test]
        public void TestRemovingNamedRegistrations()
        {
            Services
                .AddTransient<FirstService>(FirstServiceDependencyName)
                .ClearNamedRegistrations();
                
            using var serviceProvider = BuildServiceProvider();
            void GetService() => serviceProvider.GetRequiredService<FirstService>(FirstServiceDependencyName);
            
            Assert.Throws<KeyNotFoundException>(GetService);
        }
        
        [Test]
        public void TestRegistrationAfterRemovingNamedRegistrations()
        {
            void RegisterServices() => Services
                .AddTransient<FirstService>(FirstServiceDependencyName)
                .ClearNamedRegistrations()
                .AddTransient<FirstService>(FirstServiceDependencyName);

            Assert.DoesNotThrow(RegisterServices);
        }

        private void VerifyServices()
        {
            using var serviceProvider = BuildServiceProvider();

            VerifyDependency<IService>(serviceProvider, FirstServiceDependencyName, Constants.FirstServiceId);
            VerifyDependency<IService>(serviceProvider, SecondServiceDependencyName, Constants.SecondServiceId);
        }

        private void VerifyExplicitlyRegisteredServices()
        {
            using var serviceProvider = BuildServiceProvider();

            VerifyDependency<FirstService>(serviceProvider, FirstServiceDependencyName, Constants.FirstServiceId);
            VerifyDependency<SecondService>(serviceProvider, SecondServiceDependencyName, Constants.SecondServiceId);
        }

        private static void VerifyDependency<T>(
            IServiceProvider serviceProvider, string dependencyName, string serviceId)
            where T : IService
        {
            var service = serviceProvider.GetRequiredService<T>(dependencyName);
            Assert.AreSame(service.Id, serviceId);

            service = serviceProvider.GetService<T>(dependencyName);
            Assert.NotNull(service);
            Assert.AreSame(service.Id, serviceId);
        }
    }
}