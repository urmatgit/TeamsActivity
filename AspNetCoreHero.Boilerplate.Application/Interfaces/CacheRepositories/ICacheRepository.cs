using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories
{
   public interface ICacheRepositoryAsync<T> where T : class
    {
        Task<List<T>> GetCachedListAsync();

        Task<T> GetByIdAsync(int id);
    }
}
