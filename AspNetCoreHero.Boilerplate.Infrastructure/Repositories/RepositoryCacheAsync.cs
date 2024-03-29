﻿using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AspNetCoreHero.Abstractions.Domain;
using System;
using AspNetCoreHero.Boilerplate.Infrastructure.Extensions;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Repositories
{
    public class RepositoryCacheAsync<T> : IRepositoryCacheAsync<T> where T : class
    {
        private readonly AuditableIdentityContextEx _dbContext;
        IDistributedCache _distributedCache;
        Type EntityKeyType = null;
        public RepositoryCacheAsync(IDistributedCache distributedCache,AuditableIdentityContextEx dbContext)
        {
            _dbContext = dbContext;
            _distributedCache = distributedCache;
            EntityKeyType = Type.GetType($"AspNetCoreHero.Boilerplate.Infrastructure.CacheKeys.{typeof(T).Name}CacheKeys");
        }

        public IQueryable<T> Entities => _dbContext.Set<T>();

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _distributedCache.RemoveAsync(_distributedCache.GetKeyListByT<T>(EntityKeyType));
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _distributedCache.RemoveAsync(_distributedCache.GetKeyListByT<T>(EntityKeyType));
            if (entity is IBaseEntity)
                await _distributedCache.RemoveAsync(_distributedCache.GetKeyByT<T>((entity as IBaseEntity).Id, EntityKeyType));
            //return Task.CompletedTask;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext
                .Set<T>()
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).CurrentValues.SetValues(entity);
            await _distributedCache.RemoveAsync(_distributedCache.GetKeyListByT<T>(EntityKeyType));
            if (entity is IBaseEntity)
                await _distributedCache.RemoveAsync(_distributedCache.GetKeyByT<T>((entity as IBaseEntity).Id, EntityKeyType));
           // return Task.CompletedTask;
        }
    }
}