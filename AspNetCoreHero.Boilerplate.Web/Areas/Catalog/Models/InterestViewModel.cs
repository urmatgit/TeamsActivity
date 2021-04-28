using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetCoreHero.Boilerplate.Web.Areas.Catalog.Models
{
    public class InterestViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public InterestViewModel Clone()
        {
            return new InterestViewModel
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                Image = this.Image
            };
        }
        
    }
}