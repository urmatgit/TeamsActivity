using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
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
        public static InterestCheckedCachedResponse Create(Interest interestViewModel, bool check)
        {
            var interest = new InterestCheckedCachedResponse()
            {
                Id = interestViewModel.Id,
                Name = interestViewModel.Name,
                Description = interestViewModel.Description,
                Image = interestViewModel.Image,
                Check = check
            };


            return interest;
        }
    }
}
