using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StackExchange.Redis;
using Tulkas.Core.Helpers;

namespace Tulkas.Core.Data.Redis
{
    public class RedisHelper : IRedisHelper
    {
        private readonly IDatabase _db;
        private readonly IServer _server;
        
        public RedisHelper()
        {
            var redis = ConnectionMultiplexer.Connect("localhost:6379");
            _db = redis.GetDatabase(0);
            _server = redis.GetServer("localhost:6379");
        }
        
        public async Task<T> GetOrAdd<T>(KeyValuePair<string, int> cacheKey, Func<Task<T>> getAction) =>
            await GetOrAdd<T>(cacheKey.Key, cacheKey.Value, getAction); 
        
        public async Task<T> GetOrAdd<T>(string key, int cacheTime, Func<Task<T>> getAction)
        {
            var result = await Get<T>(key);
            if (result != null)
                return result;

            var getResult = await getAction();
            await Add(key, getResult, cacheTime);
            return getResult;
        }

        public async Task<bool> Add<T>(KeyValuePair<string, int> cacheKey, T value) =>
            await Add(cacheKey.Key, value, cacheKey.Value);

        public async Task<bool> Add<T>(string key, T value, int cacheTime = 10) => await _db.StringSetAsync(key,
            JsonHelper.Serialize(value), DateTime.Now.AddMinutes(cacheTime).TimeOfDay);

        public async Task<T> Get<T>(string key)
        {
            var result = await _db.StringGetAsync(key);
            if (!result.HasValue)
                return default;
            
            return JsonHelper.Deserialize<T>(result.ToString());
        }

        public async Task Delete(string key) => await _db.KeyDeleteAsync(key);
    }
}