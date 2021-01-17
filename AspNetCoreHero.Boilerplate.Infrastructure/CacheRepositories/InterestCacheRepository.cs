using AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Infrastructure.CacheRepositories
{
   public class InterestCacheRepository:CacheRepository<Interest>, IInterestCacheRepository
    {
        public InterestCacheRepository(IDistributedCache distributedCache, IRepositoryAsync<Interest> Repository): base(distributedCache,Repository)
        {
            
        }
    }
}
