using Microsoft.Extensions.DependencyInjection;
using RedisCache.Services;
using StackExchange.Redis;

namespace RedisCache
{
    public static class ServiceRegistration
    {
        public static void AddRedisCacheService(this IServiceCollection services, string redisConnectionString)
        {
            var multiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);
            services.AddSingleton<ICacheService, RedisCacheService>();
        }
    }
}
