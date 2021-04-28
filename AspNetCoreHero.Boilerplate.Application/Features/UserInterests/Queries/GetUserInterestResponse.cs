using AspNetCoreHero.Boilerplate.Application.Features.Interests.Queries.GetAllPaged;
using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Queries
{
   public class GetUserInterestResponse
    { 
        public string UserId { get; set; }
        public int InterestId { get; set; }
        public virtual GetAllInterestsResponse Interest { get; set; }
        public byte Level { get; set; }
    }
}
