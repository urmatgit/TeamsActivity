using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using AspNetCoreHero.Boilerplate.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Repositories
{
    public class UserInterestRepository : RepositoryCacheAsync<UserInterest>, IUserInterestRepository
    {
        public UserInterestRepository(IDistributedCache distributedCache, ApplicationDbContext dbContext) : base(distributedCache, dbContext)
        {

        }
        public async Task<UserInterest> GetByIdAsync(string userid, int interestid)
        {
             
            return await Entities.Include(e => e.Interest).SingleAsync(ui => ui.UserId == userid && ui.InterestId == interestid);    
        }
        /// <summary>
        /// Get interest by User id
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<List<UserInterest>> GetByIdAsync(string userid)
        {
            return await Entities.Include(e => e.Interest).Where(e => e.UserId == userid).ToListAsync();
        }
    }
}
