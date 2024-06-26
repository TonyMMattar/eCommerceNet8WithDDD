﻿/*
   This abstract base class serves as a generic repository for database operations.
   It utilizes Entity Framework Core for data access.

   The repository provides common CRUD operations and methods for retrieving lists of entities.

   Key Features:
   - Asynchronous operations for adding, updating, deleting, and saving changes.
   - Methods for getting lists of entities with or without tracking changes.
   - Support for pagination and counting total entities.

   Note:
   - The IdentityDbContext is used as the base context, assuming identity management is integrated into the application.
*/

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.Repository.Base
{
    public abstract class RepositoryBase<TEntity> where TEntity : class
    {
        #region Properties
        protected IdentityDbContext BaseContext { get; private set; }
        protected DbSet<TEntity> DbSet { get; private set; }
        #endregion

        #region Constructors
        protected RepositoryBase(IdentityDbContext context)
        {
            BaseContext = context;
            DbSet = context.Set<TEntity>();
        }
        #endregion

        #region Methods
        public virtual async Task AddAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity).ConfigureAwait(false);
        }

        public virtual async Task AddCollectionAsync(IEnumerable<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities).ConfigureAwait(false);
        }

        public virtual void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public virtual void UpdateCollection(IEnumerable<TEntity> entities)
        {
            DbSet.UpdateRange(entities);
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity != null)
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> where)
        {
            var entities = DbSet.Where(where);
            DbSet.RemoveRange(entities);
            DbSet.RemoveRange(entities);
        }

        public virtual async Task<bool> SaveChangesAsync()
        {
            var changes = await BaseContext.SaveChangesAsync().ConfigureAwait(false);
            return (changes > 0);
        }

        public virtual IQueryable<TEntity> GetList(int skip, int take)
        {
            return GetList(true).Skip(skip).Take(take);
        }

        public virtual int GetListCount()
        {
            return GetList(true).Count();
        }

        public virtual IQueryable<TEntity> GetList(bool asNoTracking = true)
        {
            return (asNoTracking) ? DbSet.AsNoTracking() : DbSet;
        }

        public virtual IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> where, bool asNoTracking = true)
        {
            return (asNoTracking) ? DbSet.AsNoTracking().Where(where) : DbSet.Where(where);
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where, bool asNoTracking = true)
        {
            return (asNoTracking) ? await DbSet.AsNoTracking().Where(where).FirstOrDefaultAsync() : await DbSet.Where(where).FirstOrDefaultAsync();
        }

        #endregion
    }
}
