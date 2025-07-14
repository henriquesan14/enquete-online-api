using AspNetCoreRateLimit;

namespace EnqueteOnline.API.Extensions
{
    public static class RateLimitingExtensions
    {
        public static IServiceCollection AddRateLimitingConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();

            services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));

            services.AddInMemoryRateLimiting();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            return services;
        }
    }
}
