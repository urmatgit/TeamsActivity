using AspNetCoreHero.Boilerplate.Application.Features.Interests.Queries.GetAllCached;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.UserInterests.Commands.Edit
{
   public class EditUserInterestsResponse
    {
        public string UserId { get; set; }
        public IList<InterestCheckedCachedResponse> Interests { get; set; }
    }
}
