using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Areas.Catalog.Models
{
    public class CheckableInterestViewModel: InterestViewModel
    {
        public bool Check { get; set; }
        public static CheckableInterestViewModel Create(InterestViewModel interestViewModel,bool check)
        {
            var interest = new CheckableInterestViewModel()
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
