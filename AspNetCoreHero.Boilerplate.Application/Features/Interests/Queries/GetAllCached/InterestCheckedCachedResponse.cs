using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Interests.Queries.GetAllCached
{
    public class InterestCheckedCachedResponse: InterestsCachedResponse
    {
        public bool Check { get; set; }
    }
}
