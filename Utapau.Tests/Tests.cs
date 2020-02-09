using System;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Utapau.Tests.Services;

namespace Utapau.Tests
{
    public class Tests
    {
        private const string FirstServiceDependencyName = nameof(FirstService);
        private const string SecondServiceDependencyName = nameof(SecondService);
        
        private IServiceCollection _services;
        
        [SetUp]
        public void Setup()
        {
            _services = new ServiceCollection();
        }

        [Test]
        public void TestSingleton()
        {
            _services
                .AddSingleton<IService, FirstService>(FirstServiceDependencyName)
                .AddSingleton<IService, SecondService>(SecondServiceDependencyName);

            VerifyServices();
        }
        
        [Test]
        public void TestScoped()
        {
            _services
                .AddScoped<IService, FirstService>(FirstServiceDependencyName)
                .AddScoped<IService, SecondService>(SecondServiceDependencyName);

            VerifyServices();
        }
        
        [Test]
        public void TestTransient()
        {
            _services
                .AddTransient<IService, FirstService>(FirstServiceDependencyName)
                .AddTransient<IService, SecondService>(SecondServiceDependencyName);

            VerifyServices();
        }

        private void VerifyServices()
        {
            using var serviceProvider = _services.BuildServiceProvider();
            
            VerifyDependency(serviceProvider, FirstServiceDependencyName, Constants.FirstServiceId);
            VerifyDependency(serviceProvider, SecondServiceDependencyName, Constants.SecondServiceId);
        }
        
        private static void VerifyDependency(IServiceProvider serviceProvider, string dependencyName, string serviceId)
        {
            var service = serviceProvider.GetRequiredService<IService>(dependencyName);
            Assert.AreSame(service.Id, serviceId);
            
            service = serviceProvider.GetService<IService>(dependencyName);
            Assert.NotNull(service);
            Assert.AreSame(service.Id, serviceId);
        }
    }
}