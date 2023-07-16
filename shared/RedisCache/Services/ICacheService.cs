namespace RedisCache.Services
{
    public interface ICacheService
    {
        /// <summary>
        /// Get value by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> GetValueAsync(string key);

        /// <summary>
        /// Set redis key value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireTime">If this param is null then it will be setted 1 hour.</param>
        /// <returns></returns>
        Task<bool> SetValueAsync(string key, string value, TimeSpan? timeSpan = null);

        /// <summary>
        /// Set redis key values
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireTime">If this param is null then it will be setted 1 hour.</param>
        /// <returns></returns>
        Task<bool> SetValueAsync<T>(string key, Func<Task<T>> action, TimeSpan? timeSpan = null) where T : class;

        /// <summary>
        /// Get by redis cache. If there is no data related with key then create new key and set value async way.
        /// </summary>
        /// <typeparam name="T">T is model that is serializable data</typeparam>
        /// <param name="key"></param>
        /// <param name="action"></param>
        /// <param name="expireTime">If this param is null then it will be setted 1 hour.</param>
        /// <returns></returns>
        Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> action, TimeSpan? timeSpan = null) where T : class;

        /// <summary>
        /// Get by redis cache. If there is no data related with key then create new key and set value.
        /// </summary>
        /// <typeparam name="T">T is model that is serializable data</typeparam>
        /// <param name="key"></param>
        /// <param name="action"></param>
        /// <param name="expireTime">If this param is null then it will be setted 1 hour.</param>
        /// <returns></returns>
        T GetOrSet<T>(string key, Func<T> action, TimeSpan? timeSpan = null) where T : class;

        /// <summary>
        /// Clear cache by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task Clear(string key);

        /// <summary>
        /// Clear all cache
        /// </summary>
        void ClearAll();
    }
}
