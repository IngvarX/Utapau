using System;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Utapau.Tests.Services;

namespace Utapau.Tests
{
    public class WarmupTests
    {
        private IServiceCollection _services;
        
        [SetUp]
        public void Setup()
        {
            _services = new ServiceCollection();
        }

        [Test]
        public void TestWarmup()
        {
            _services
                .AddSingleton<FirstService>()
                .AddSingleton<SecondService>()
                .AddSingleton<ThirdService>();

            void ResolveAllServicesAction() => _services.ResolveAllServices();
            
            Assert.DoesNotThrow(ResolveAllServicesAction);
        }
        
        [Test]
        public void TestWarmupException()
        {
            _services
                .AddSingleton<FirstService>()
                .AddSingleton<ThirdService>();

            void ResolveAllServicesAction() => _services.ResolveAllServices();
            
            Assert.Throws<InvalidOperationException>(ResolveAllServicesAction);
        }
    }
}