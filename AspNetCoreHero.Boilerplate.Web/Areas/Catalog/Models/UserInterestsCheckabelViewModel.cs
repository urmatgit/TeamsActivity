using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Areas.Catalog.Models
{
    public class UserInterestsCheckabelViewModel
    {
        public IList<CheckableInterestViewModel> Interestes { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
