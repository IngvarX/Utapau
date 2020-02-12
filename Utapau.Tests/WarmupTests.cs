using System;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Utapau.ServiceResolver;
using Utapau.Tests.Services;

namespace Utapau.Tests
{
    public class WarmupTests : TestsBase
    {
        [Test]
        public void TestDefaultRegistrations()
        {
            Assert.DoesNotThrow(ResolveAllServices);
        }

        [Test]
        public void TestWarmup()
        {
            Services
                .AddSingleton<FirstService>()
                .AddSingleton<SecondService>()
                .AddSingleton<ThirdService>();
            
            Assert.DoesNotThrow(ResolveAllServices);
        }
        
        [Test]
        public void TestWarmupException()
        {
            Services
                .AddSingleton<FirstService>()
                .AddSingleton<ThirdService>();
            
            Assert.Throws<InvalidOperationException>(ResolveAllServices);
        }
        
        [Test]
        public void TestFilteredWarmup()
        {
            Services
                .AddSingleton<FirstService>()
                .AddSingleton<ThirdService>();
            
            Assert.DoesNotThrow(ResolveFirstService);
        }

        private void ResolveAllServices() => Services.ResolveAllServices();

        private void ResolveFirstService() => Services.ResolveAllServicesWhere(t => t == typeof(FirstService));
    }
}