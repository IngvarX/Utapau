using System;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Utapau.Providers;
using Utapau.Tests.Services;

namespace Utapau.Tests
{
    public class FactoryRegistrationTests : TestsBase
    {

        [Test]
        public void TestFactory()
        {
            Services
                .AddScoped<IService, SecondService>()
                .AddFactory<IService>();

            using var serviceProvider = BuildServiceProvider();

            var serviceFactory = serviceProvider.GetRequiredService<Func<IService>>();
            Assert.NotNull(serviceFactory);

            var service = serviceFactory();
            Assert.NotNull(service);
            Assert.True(service is SecondService);
        }

        [Test]
        public void TestFactoryException()
        {
            void RegisterFactoryAction() => Services.AddFactory<IService>();
            
            Assert.Throws<InvalidOperationException>(RegisterFactoryAction);
        }
    }
}