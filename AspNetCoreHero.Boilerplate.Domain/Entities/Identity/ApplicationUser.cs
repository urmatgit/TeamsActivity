using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AspNetCoreHero.Boilerplate.Domain.Entities.Identity 
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] ProfilePicture { get; set; }
        public bool IsActive { get; set; } = false;
        
        public string AboutMe { get; set; }
        [JsonIgnore]
        public virtual IList<UserInterest> UserInterests { get; set; }
    }
}