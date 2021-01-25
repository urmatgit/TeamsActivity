using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories
{
    public interface IUserInterestRepository: IRepositoryCacheAsync<UserInterest>
    {
        Task<UserInterest> GetByIdAsync(string userid,int interestid);
        Task<List<UserInterest>> GetByIdAsync(string userid);
    }
}
