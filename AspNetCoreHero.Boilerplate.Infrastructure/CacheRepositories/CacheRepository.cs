using AspNetCoreHero.Abstractions.Repository;
using AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.ThrowR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Extensions.Caching;

namespace AspNetCoreHero.Boilerplate.Infrastructure.CacheRepositories
{
   public class CacheRepository<T> : ICacheRepositoryAsync<T>  where T : class
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IRepositoryAsync<T> _repository;

        public CacheRepository(IDistributedCache distributedCache, IRepositoryAsync<T> Repository)
        {
            _distributedCache = distributedCache;
            _repository = Repository;
        }
        private string getKeyName(int id)
        {
            Type type =Type.GetType($"{typeof(T).Name}CacheKeys");
            if (type==null)
                Throw.Exception.IfNull($"{typeof(T).Name}CacheKeys", typeof(T).Name, "Not  Found");
            var getKeyMethod = type.GetMethod("GetKey");
            var result =(string)getKeyMethod.Invoke(null, new object[] { id });
            return result;

        }
        private string getKeyListName()
        {
            Type type = Type.GetType($"{typeof(T).Name}CacheKeys");
            if (type == null)
                Throw.Exception.IfNull($"{typeof(T).Name}CacheKeys", typeof(T).Name, "Not  Found");
            var property= type.GetProperty("ListKey");
            return (string)property.GetValue(null, null);
        }
        public async Task<T> GetByIdAsync(int id)
        {
            string cacheKey = getKeyName(id);// BrandCacheKeys.GetKey(id);
            var brand = await _distributedCache.GetAsync<T>(cacheKey);
            if (brand == null)
            {
                brand = await _repository.GetByIdAsync(id);
                Throw.Exception.IfNull(brand, typeof(T).Name, "No Brand Found");
                await _distributedCache.SetAsync(cacheKey, brand);
            }
            return brand;
        }

        public async Task<List<T>> GetCachedListAsync()
        {
            string cacheKey = getKeyListName();// BrandCacheKeys.ListKey;
            var brandList = await _distributedCache.GetAsync<List<T>>(cacheKey);
            if (brandList == null)
            {
               var brandList1 = await _repository.GetAllAsync();
                await _distributedCache.SetAsync(cacheKey, brandList);
            }
            return brandList;
        }
    }
}
