using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Utapau.Providers;
using Utapau.Providers.Interfaces;
using Utapau.Tests.Services;

namespace Utapau.Tests
{
    public class ProviderTests : TestsBase
    {
        [Test]
        public void TestProvider()
        {
            Services
                .AddScoped<IService, FirstService>()
                .AddProvider<IService>()
                .AddSingleton(sp =>
                {
                    var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
                    httpContextAccessorMock
                        .SetupGet(m => m.HttpContext.RequestServices)
                        .Returns(sp);

                    return httpContextAccessorMock.Object;
                });

            using var serviceProvider = BuildServiceProvider();

            var provider = serviceProvider.GetRequiredService<IProvider<IService>>();
            Assert.NotNull(provider);

            var service = provider.Instance;
            Assert.NotNull(service);
            Assert.True(service is FirstService);
        }
        
        [Test]
        public void TestProviderException()
        {
            void RegisterProviderAction() => Services.AddProvider<IService>();
            
            Assert.Throws<InvalidOperationException>(RegisterProviderAction);
        }
    }
}