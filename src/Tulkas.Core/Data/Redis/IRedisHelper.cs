using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tulkas.Core.Data.Redis
{
    public interface IRedisHelper
    {
        Task<T> GetOrAdd<T>(KeyValuePair<string, int> cacheKey, Func<Task<T>> getAction);
        Task<T> GetOrAdd<T>(string key, int cacheTime, Func<Task<T>> getAction);
        Task<bool> Add<T>(KeyValuePair<string, int> cacheKey, T value);
        Task<bool> Add<T>(string key, T value, int cacheTime = 10);
        Task<T> Get<T>(string key);
        Task Delete(string key);
    }
}