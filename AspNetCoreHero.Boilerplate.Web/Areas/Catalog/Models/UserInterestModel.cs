

namespace AspNetCoreHero.Boilerplate.Web.Areas.Catalog.Models
{
    public class UserInterestModel
    {
        public string UserId { get; set; }
        public int InterestId { get; set; }
        public virtual InterestViewModel Interest { get; set; }
        public byte Level { get; set; }
    }
}