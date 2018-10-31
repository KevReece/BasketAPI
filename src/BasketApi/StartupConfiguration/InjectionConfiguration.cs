using BasketApi.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BasketApi.StartupConfiguration
{
    public static class InjectionConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<BasketService>();
        }
    }
}