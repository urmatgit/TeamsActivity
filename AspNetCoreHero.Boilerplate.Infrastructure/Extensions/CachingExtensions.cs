using AspNetCoreHero.ThrowR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Extensions
{
    public static class Caching
    {
        public static int CacheExpirationInMinutes { get; set; } = 30;
        public static string GetKeyListByT<T>(this IDistributedCache distributedCache, Type type)
        {
            //   Type type = Type.GetType($"AspNetCoreHero.Boilerplate.Infrastructure.CacheKeys.{typeof(T).Name}CacheKeys");
            if (type == null)
                Throw.Exception.IfNull(type, typeof(T).Name, "Not  Found");
            var property = type.GetProperty("ListKey");
            return (string)property.GetValue(null, null);
        }

        public static string GetKeyByT<T>(this IDistributedCache distributedCache, int id, Type type)
        {
            //Type type = Type.GetType($"AspNetCoreHero.Boilerplate.Infrastructure.CacheKeys.{typeof(T).Name}CacheKeys");
            if (type == null)
                Throw.Exception.IfNull(type, typeof(T).Name, "Not  Found");
            var getKeyMethod = type.GetMethod("GetKey");
            var result = (string)getKeyMethod.Invoke(null, new object[] { id });
            return result;
        }
        public static async Task<T> GetAsync<T>(this IDistributedCache distributedCache, string cacheKey, CancellationToken token = default)
        {
            Throw.Exception.IfNull(distributedCache, nameof(distributedCache));
            Throw.Exception.IfNull(cacheKey, nameof(cacheKey));
            byte[] utf8Bytes = await distributedCache.GetAsync(cacheKey, token).ConfigureAwait(false);
            if (utf8Bytes != null)
            {
                return JsonSerializer.Deserialize<T>(utf8Bytes);
            }
            return default;
        }
        public static async Task RemoveAsync(this IDistributedCache distributedCache, string cacheKey, CancellationToken token = default)
        {
            Throw.Exception.IfNull(distributedCache, nameof(distributedCache));
            Throw.Exception.IfNull(cacheKey, nameof(cacheKey));
            await distributedCache.RemoveAsync(cacheKey, token).ConfigureAwait(false);
        }
        public static async Task SetAsync<T>(this IDistributedCache distributedCache, string cacheKey, T obj, int? cacheExpirationInMinutes = null, CancellationToken token = default)
        {
            Throw.Exception.IfNull(distributedCache, nameof(distributedCache));
            Throw.Exception.IfNull(cacheKey, nameof(cacheKey));
            Throw.Exception.IfNull(obj, nameof(obj));
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
             .SetSlidingExpiration(TimeSpan.FromMinutes(cacheExpirationInMinutes ?? CacheExpirationInMinutes));
            byte[] utf8Bytes = JsonSerializer.SerializeToUtf8Bytes<T>(obj);
            await distributedCache.SetAsync(cacheKey, utf8Bytes, options, token).ConfigureAwait(false);
        }
    }
}
