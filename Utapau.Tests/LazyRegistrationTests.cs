using System;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Utapau.Providers;
using Utapau.Tests.Services;

namespace Utapau.Tests
{
    public class LazyRegistrationTests : TestsBase
    {
        [Test]
        public void TestLazy()
        {
            Services
                .AddScoped<IService, SecondService>()
                .AddLazy<IService>();

            using var serviceProvider = BuildServiceProvider();

            var lazyFactory = serviceProvider.GetRequiredService<Lazy<IService>>();
            Assert.NotNull(lazyFactory);

            var service = lazyFactory.Value;
            Assert.NotNull(service);
            Assert.True(service is SecondService);
        }

        [Test]
        public void TestLazyException()
        {
            void RegisterLazyAction() => Services.AddLazy<IService>();
            
            Assert.Throws<InvalidOperationException>(RegisterLazyAction);
        }
    }
}