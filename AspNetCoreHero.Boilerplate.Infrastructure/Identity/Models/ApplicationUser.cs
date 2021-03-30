using Microsoft.AspNetCore.Identity;

namespace AspNetCoreHero.Boilerplate.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] ProfilePicture { get; set; }
        public bool IsActive { get; set; } = false;
        
        public string AboutMe { get; set; }
    }
}