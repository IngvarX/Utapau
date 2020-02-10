using System;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
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

        private void ResolveAllServices() => Services.ResolveAllServices();
    }
}