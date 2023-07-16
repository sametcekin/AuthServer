using StackExchange.Redis;
using System.Text.Json;

namespace RedisCache.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _redisCon;
        private readonly IDatabase _cache;
        public RedisCacheService(IConnectionMultiplexer redisCon)
        {
            _redisCon = redisCon;
            _cache = redisCon.GetDatabase();
        }

        public async Task<string> GetValueAsync(string key)
        {
            return await _cache.StringGetAsync(key);
        }

        /// <summary>
        /// Set redis key value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireTime">If this param is null then it will be setted 1 hour.</param>
        /// <returns></returns>
        public async Task<bool> SetValueAsync(string key, string value, TimeSpan? expireTime = null)
        {
            var expireIn = expireTime ?? TimeSpan.FromHours(1);
            return await _cache.StringSetAsync(key, value, expireIn);
        }

        /// <summary>
        /// Get by redis cache. If there is no data related with key then create new key and set value async way.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="action"></param>
        /// <param name="expireTime">If this param is null then it will be setted 1 hour.</param>
        /// <returns></returns>
        public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> action, TimeSpan? expireTime = null) where T : class
        {
            var expireIn = expireTime ?? TimeSpan.FromHours(1);
            var result = await _cache.StringGetAsync(key);
            if (result.IsNull)
            {
                result = JsonSerializer.SerializeToUtf8Bytes(await action());
                await SetValueAsync(key, result, expireIn);
            }
            return JsonSerializer.Deserialize<T>(result);
        }

        /// <summary>
        /// Get by redis cache. If there is no data related with key then create new key and set value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="action"></param>
        /// <param name="expireTime">If this param is null then it will be setted 1 hour.</param>
        /// <returns></returns>
        public T GetOrSet<T>(string key, Func<T> action, TimeSpan? expireTime = null) where T : class
        {
            var expireIn = expireTime ?? TimeSpan.FromHours(1);
            var result = _cache.StringGet(key);
            if (result.IsNull)
            {
                result = JsonSerializer.SerializeToUtf8Bytes(action());
                _cache.StringSet(key, result, expireIn);
            }
            return JsonSerializer.Deserialize<T>(result);
        }

        public async Task Clear(string key)
        {
            await _cache.KeyDeleteAsync(key);
        }

        public void ClearAll()
        {
            var endpoints = _redisCon.GetEndPoints(true);
            foreach (var endpoint in endpoints)
            {
                var server = _redisCon.GetServer(endpoint);
                server.FlushAllDatabases();
            }
        }
    }
}
