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
        Type EntityKeyType = null;
        public CacheRepository(IDistributedCache distributedCache, IRepositoryAsync<T> Repository)
        {
            _distributedCache = distributedCache;
            _repository = Repository;
            EntityKeyType = Type.GetType($"AspNetCoreHero.Boilerplate.Infrastructure.CacheKeys.{typeof(T).Name}CacheKeys");
        }
        
        public async Task<T> GetByIdAsync(int id)
        {
            string cacheKey = _distributedCache.GetKeyByT<T>(id,EntityKeyType);// BrandCacheKeys.GetKey(id);
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
            string cacheKey = _distributedCache.GetKeyListByT<T>(EntityKeyType);// getKeyListName();// BrandCacheKeys.ListKey;
            var brandList = await _distributedCache.GetAsync<List<T>>(cacheKey);
            if (brandList == null)
            {
                brandList = await _repository.GetAllAsync();
                await _distributedCache.SetAsync(cacheKey, brandList);
            }
            return brandList;
        }
    }
}
