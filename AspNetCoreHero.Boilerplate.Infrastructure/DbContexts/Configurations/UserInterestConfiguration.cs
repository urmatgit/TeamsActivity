using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Infrastructure.DbContexts.Configurations
{
    public class UserInterestConfiguration : IEntityTypeConfiguration<UserInterest>
    {
        public void Configure(EntityTypeBuilder<UserInterest> builder)
        {
            builder.HasKey(tp => new { tp.UserId, tp.InterestId });
            builder.HasOne(i => i.Interest).WithMany(i => i.UserInterests).HasForeignKey(ui => ui.InterestId);
            
        }
    }
}
