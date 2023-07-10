using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedisCache.Services;
using StackExchange.Redis;

namespace RedisCache
{
    public static class ServiceRegistration
    {
        public static void AddRedisCacheService(this IServiceCollection services, IConfiguration configuration)
        {
            var multiplexer = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);
            services.AddSingleton<ICacheService, RedisCacheService>();
        }
    }
}
