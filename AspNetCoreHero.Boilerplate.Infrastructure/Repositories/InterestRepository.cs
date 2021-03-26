using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using AspNetCoreHero.Boilerplate.Infrastructure.DbContexts;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Repositories
{
   public class InterestRepository: RepositoryCacheAsync<Interest>, IInterestRepository
    {
        public InterestRepository(IDistributedCache distributedCache,AuditableIdentityContextEx dbContext): base(distributedCache,dbContext)
        {
        
        }
    }
}
