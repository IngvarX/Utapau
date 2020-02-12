using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Utapau.Providers.Interfaces;

namespace Utapau.Providers.Implementations
{
    internal class Provider<TService> : IProvider<TService>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TService Instance => GetInstance();

        public Provider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        private TService GetInstance()
        {
            var serviceProvider = _httpContextAccessor.HttpContext.RequestServices;

            return serviceProvider.GetRequiredService<TService>();
        }
    }
}