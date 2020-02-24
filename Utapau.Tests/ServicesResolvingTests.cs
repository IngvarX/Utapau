using System;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Utapau.ServiceResolver;
using Utapau.Tests.Services;

namespace Utapau.Tests
{
    public class ServicesResolvingTests : TestsBase
    {
        [Test]
        public void TestDefaultRegistrations()
        {
            Assert.DoesNotThrow(ResolveAllServices);
        }

        [Test]
        public void TestResolve()
        {
            Services
                .AddSingleton<FirstService>()
                .AddSingleton<SecondService>()
                .AddSingleton<ThirdService>();
            
            Assert.DoesNotThrow(ResolveAllServices);
        }
        
        [Test]
        public void TestResolveException()
        {
            Services
                .AddSingleton<FirstService>()
                .AddSingleton<ThirdService>();
            
            Assert.Throws<InvalidOperationException>(ResolveAllServices);
        }
        
        [Test]
        public void TestFilteredResolving()
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