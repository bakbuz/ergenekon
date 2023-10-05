using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace Ergenekon.Infrastructure.Services;

public static class CacheExt
{
    public static T GetData<T>(this IDistributedCache distributedCache, string key, Func<T> acquire, int cacheTimeInMinute = 15)
    {
        var cachedValue = distributedCache.GetString(key);

        //item already is in cache, so return it
        if (cachedValue != null && cachedValue.Length > 0)
            return distributedCache.GetDataAsync<T>(key).Result;

        //or create it using passed function
        var result = acquire();

        //and set in cache (if cache time is defined)
        if (cacheTimeInMinute > 0)
            distributedCache.SetDataAsync(key, result, cacheTimeInMinute).Wait();

        return result;
    }

    public static async Task<T> GetDataAsync<T>(this IDistributedCache distributedCache, string key, Func<T> acquire, int cacheTimeInMinute = 15)
    {
        var cachedValue = distributedCache.GetString(key);

        //item already is in cache, so return it
        if (cachedValue != null && cachedValue.Length > 0)
            return await distributedCache.GetDataAsync<T>(key);

        //or create it using passed function
        var result = acquire();

        //and set in cache (if cache time is defined)
        if (cacheTimeInMinute > 0)
            await distributedCache.SetDataAsync(key, result, cacheTimeInMinute);

        return result;
    }





    private static async Task<T> GetDataAsync<T>(this IDistributedCache distributedCache, string key)
    {
        //get serialized item from cache
        string? serializedItem = await distributedCache.GetStringAsync(key);
        if (serializedItem == null)
            return default(T);

        //deserialize item
        var data = JsonSerializer.Deserialize<T>(serializedItem);
        if (data == null)
            return default(T);

        return data;
    }

    private static async Task SetDataAsync(this IDistributedCache distributedCache, string key, object data, int cacheTimeInMinute)
    {
        if (data == null)
            return;

        //set cache time
        var expiresIn = new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTime.UtcNow.Add(TimeSpan.FromMinutes(cacheTimeInMinute))
        };

        //serialize item
        var serializedItem = JsonSerializer.Serialize(data);

        // byte array
        var value = Encoding.UTF8.GetBytes(serializedItem);

        //and set it to cache
        await distributedCache.SetAsync(key, value, expiresIn);
    }
}
