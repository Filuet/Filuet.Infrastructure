﻿using Filuet.Infrastructure.DataProvider.Entities;
using Filuet.Infrastructure.DataProvider.Interfaces;
using Filuet.Infrastructure.DataProvider.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Filuet.Infrastructure.DataProvider
{
    public class CommonRepository<TDbContext, TEntity, TKey> : ICommonRepository<TEntity>
          where TDbContext : DbContext
          where TEntity : Entity<TKey>
    {
        public string Guid = System.Guid.NewGuid().ToString();

        public bool IsAutoSave { get; protected set; }
        public TDbContext DbContext { get; protected set; }
        public DbSet<TEntity> DbSet { get; protected set; }

        public CommonRepository(TDbContext dbContext)
        {
            IsAutoSave = false;

            DbContext = dbContext ?? throw new ArgumentNullException("dbContext");
            DbSet = DbContext.Set<TEntity>();
        }

        protected virtual IQueryable<TEntity> QueryUntracking => DbSet.AsNoTracking();

        protected virtual IQueryable<TEntity> QueryTracking => DbSet;

        #region IRepository<T>
        public IEnumerable<TEntity> Get(Predicate<TEntity> predicate = null, int? count = null)
        {
            if (!count.HasValue)
                return QueryUntracking.Where(t => predicate == null ? true : predicate(t));
            else return QueryUntracking.Where(t => predicate == null ? true : predicate(t)).Take(count.Value);
        }

        protected virtual int SaveChanges() => DbContext.SaveChanges();

        public virtual IEnumerable<TEntity> GetAll() => QueryUntracking.ToList();

        public virtual IEnumerable<TEntity> Add(IEnumerable<TEntity> entities)
        {
            var result = new List<TEntity>();

            if (entities != null)
                foreach (var entity in entities)
                    result.Add(Add(entity));

            return result;
        }

        public virtual TEntity Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity", "Can't add null Entity to DbContext");

            EntityEntry dbEntityEntry = DbContext.Entry(entity);

            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
            }

            if (IsAutoSave)
                try
                {
                    SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    DbContext.Remove(entity);
                    throw ex;
                }

            return entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("Entity", "Can't update null Entity to DbContext");

                EntityEntry dbEntityEntry = DbContext.Entry(entity);

               // if (dbEntityEntry.State == EntityState.Detached)
                    DbSet.Attach(entity);

                //      DbSet.Update(entity);
                dbEntityEntry.State = EntityState.Modified;

                if (IsAutoSave)
                    SaveChanges();

                DbContext.Entry(entity).State = EntityState.Detached;

                return entity;
            }
            catch (Exception)
            {
                DbContext.Entry(entity).State = EntityState.Detached;
                DbContext.Entry(entity).State = EntityState.Modified;
                DbContext.SaveChanges();
                return entity;
            }
        }

        public virtual TEntity Modify(TEntity entity)
        {
            if (!entity.ID.Equals(default(TKey)))
                return Update(entity);

            return Add(entity);
        }

        public virtual void Restore(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity", "Can't restore null Entity to DbContext");

            if (entity is IDeletable)
            {
                ((IDeletable)entity).Restore();
                Update(entity);
                SaveChanges();
                return;
            }
            else
                throw new System.Exception("Can't restore not IDeletable Entity!");
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity", "Can't delete null Entity to DbContext");

            if (entity is IDeletable)
            {
                ((IDeletable)entity).MarkDeleted();
                Update(entity);
                SaveChanges();
                return;
            }

            DeleteReferences(entity);

            EntityEntry dbEntityEntry = DbContext.Entry(entity);

            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                try
                {
                    DbSet.Attach(entity);
                }
                catch { }
                DbSet.Remove(entity);
            }

            if (IsAutoSave)
                SaveChanges();
        }

        public virtual void Delete(TKey id)
        {
            var entity = Get(id);

            // if not found - assume already deleted...
            if (entity == null) return;

            Delete(entity);
        }

        protected virtual void DeleteReferences(TEntity entity) { }

        public virtual void Refresh(TEntity entity)
        {
            if (entity != null)
            {
                var entry = DbContext.Entry(entity);
                if (entry.State != EntityState.Detached && entry.State != EntityState.Added)
                    entry.Reload();
            }
        }
        #endregion

        #region Functions
        protected TEntity GetEntityByKey(IQueryable<TEntity> query, object key)
        {
            if (key == null)
                return null;

            if (key is TKey)
                return query.SingleOrDefault(BuildFindExpression("ID", key));
            else
            {
                var code = key.ToString().Trim();
                if (!string.IsNullOrEmpty(code))
                {
                    if (typeof(IGuidable).IsAssignableFrom(typeof(TEntity)))
                        return query.SingleOrDefault(this.BuildFindExpression("UID", code));
                }
            }

            return null;
        }

        protected void LoadNavigation(TEntity entity, Expression<Func<TEntity, object>> navigation)
        {
            var entry = DbContext.Entry(entity);
            if (entity != null && navigation != null)
                entry.Reference(navigation).Load();
        }

        protected void LoadCollection(TEntity entity, Expression<Func<TEntity, IEnumerable<object>>> collection)
        {
            var entry = DbContext.Entry(entity);
            if (entity != null && collection != null)
                entry.Collection(collection).Load();
        }

        /// <summary>
        /// Builds Lambda Expression for IQuerable&lt;T&gt; (DbSet) to Find the entity by Field and Value
        /// </summary>
        /// <param name="field">Name of the field to search</param>
        /// <param name="value">Value of the field to search</param>
        /// <returns>Lambda Expression to make a query</returns>
        protected Expression<Func<TEntity, bool>> BuildFindExpression(string field, object value)
        {
            var entityExpression = Expression.Parameter(typeof(TEntity));

            var fieldExpression = Expression.Equal(Expression.Property(entityExpression, field), Expression.Constant(value));

            return Expression.Lambda<Func<TEntity, bool>>(fieldExpression, entityExpression);
        }

        public TEntity Get(object id, bool tracking = true)
        {
            var result = GetEntityByKey(tracking ? QueryTracking : QueryUntracking, id);

            if (result is IDeletable && ((IDeletable)result).Deleted)
                return null;

            //DbContext.Entry(result).State = EntityState.Detached;
            return result;
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
            => QueryUntracking.Where(predicate);
        #endregion
    }
}