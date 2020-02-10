using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Utapau.Tests
{
    public class TestsBase
    {
        protected IServiceCollection Services;

        [SetUp]
        public void Setup()
        {
            Services = new ServiceCollection();
        }
        
        [TearDown]
        public void TearDown()
        {
            Services.ClearNamedRegistrations();
        }

        protected ServiceProvider BuildServiceProvider() => Services.BuildServiceProvider();
    }
}