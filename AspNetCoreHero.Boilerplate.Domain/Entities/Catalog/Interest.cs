using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Domain.Entities.Catalog
{
    public class Interest : BaseEntity
    {
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public virtual IList<UserInterest> UserInterests { get; set; }
    }
}
