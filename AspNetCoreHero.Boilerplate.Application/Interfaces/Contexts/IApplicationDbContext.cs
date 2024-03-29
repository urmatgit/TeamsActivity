﻿using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.Contexts
{
    public interface IAuditableIdentityContextEx
    {
        IDbConnection Connection { get; }
        bool HasChanges { get; }

        EntityEntry Entry(object entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbSet<Product> Products { get; set; }
        DbSet<Interest> Interests { get; set; }
        DbSet<UserInterest> UserInterests { get; set; }
    }
}