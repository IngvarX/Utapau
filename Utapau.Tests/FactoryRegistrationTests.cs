using System;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Utapau.Tests.Services;

namespace Utapau.Tests
{
    public class FactoryRegistrationTests
    {
        private IServiceCollection _services;

        [SetUp]
        public void Setup()
        {
            _services = new ServiceCollection();
        }

        [Test]
        public void TestFactory()
        {
            _services
                .AddScoped<IService, SecondService>()
                .AddFactory<IService>();

            using var serviceProvider = _services.BuildServiceProvider();

            var serviceFactory = serviceProvider.GetRequiredService<Func<IService>>();
            Assert.NotNull(serviceFactory);

            var service = serviceFactory();
            Assert.NotNull(service);
            Assert.True(service is SecondService);
        }

        [Test]
        public void TestFactoryException()
        {
            void RegisterFactoryAction() => _services.AddFactory<IService>();
            
            Assert.Throws<InvalidOperationException>(RegisterFactoryAction);
        }
    }
}