using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetCoreHero.Boilerplate.Web.Areas.Catalog.Models
{
    public class InterestViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        
    }
}