using Filuet.Infrastructure.DataProvider.Entities;
using Filuet.Infrastructure.DataProvider.Interfaces;
using Filuet.Infrastructure.DataProvider.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
        public IEnumerable<TEntity> GetExactly(Predicate<TEntity> predicate, int count)
            => QueryUntracking.Where(t => predicate == null ? true : predicate(t)).Take(count);

        public async Task<IEnumerable<TEntity>> GetExactlyAsync(Predicate<TEntity> predicate, int count)
            => await QueryUntracking.Where(t => predicate == null ? true : predicate(t)).Take(count).ToListAsync();

        protected virtual int SaveChanges() => DbContext.SaveChanges();

        protected virtual async Task<int> SaveChangesAsync() => await DbContext.SaveChangesAsync();

        public virtual IEnumerable<TEntity> GetAll(bool tracking = false) => tracking ? QueryTracking.ToList() : QueryUntracking.ToList();

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = false) => tracking ? await QueryTracking.ToListAsync() : await QueryUntracking.ToListAsync();

        public virtual IEnumerable<TEntity> Add(IEnumerable<TEntity> entities)
        {
            var result = new List<TEntity>();

            if (entities != null)
                foreach (var entity in entities)
                    result.Add(Add(entity));

            return result;
        }

        public virtual async Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities)
        {
            var result = new List<TEntity>();

            if (entities != null)
                foreach (var entity in entities)
                    result.Add(await AddAsync(entity));

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

        public virtual async Task<TEntity> AddAsync(TEntity entity)
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
                await DbSet.AddAsync(entity);
            }

            if (IsAutoSave)
                try
                {
                    await SaveChangesAsync();
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

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
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
                    await SaveChangesAsync();

                DbContext.Entry(entity).State = EntityState.Detached;

                return entity;
            }
            catch (Exception)
            {
                DbContext.Entry(entity).State = EntityState.Detached;
                DbContext.Entry(entity).State = EntityState.Modified;
                await DbContext.SaveChangesAsync();
                return entity;
            }
        }

        public virtual async Task<ICollection<TEntity>> UpdateScopeAsync(ICollection<TEntity> entities)
        {
            try
            {
                if (!entities.Any())
                    return entities;

                foreach (TEntity entity in entities)
                {
                    EntityEntry dbEntityEntry = DbContext.Entry(entity);
                    DbSet.Attach(entity);
                    dbEntityEntry.State = EntityState.Modified;
                }

                if (IsAutoSave)
                    await SaveChangesAsync();

                foreach (TEntity entity in entities)
                    DbContext.Entry(entity).State = EntityState.Detached;

                return entities;
            }
            catch (Exception)
            {
                foreach (TEntity entity in entities)
                {
                    DbContext.Entry(entity).State = EntityState.Detached;
                    DbContext.Entry(entity).State = EntityState.Modified;
                }

                await DbContext.SaveChangesAsync();

                return entities;
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

        public virtual async Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity", "Can't delete null Entity to DbContext");

            if (entity is IDeletable)
            {
                ((IDeletable)entity).MarkDeleted();
                await UpdateAsync(entity);
                await SaveChangesAsync();
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
                await SaveChangesAsync();
        }


        public virtual async Task DeleteScopeAsync(ICollection<TEntity> entities)
        {
            if (!entities.Any())
                return;

            if (entities.First() is IDeletable)
            {
                foreach (var entity in entities)
                    ((IDeletable)entity).MarkDeleted();

                await UpdateScopeAsync(entities);
                if (IsAutoSave)
                    await SaveChangesAsync();
                return;
            }

            DeleteReferences(entities);

            foreach (var entity in entities)
            {
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
            }

            if (IsAutoSave)
                await SaveChangesAsync();
        }

        public virtual void Delete(TKey id)
        {
            var entity = Get(id);

            // if not found - assume already deleted...
            if (entity == null) return;

            Delete(entity);
        }

        protected virtual void DeleteReferences(TEntity entity) { }

        protected virtual void DeleteReferences(ICollection<TEntity> entities) { }

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

            return result;
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool tracking = false)
            => tracking ? QueryTracking.Where(predicate) : QueryUntracking.Where(predicate);

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = false)
            => tracking ? await QueryTracking.Where(predicate).ToListAsync() : await QueryUntracking.Where(predicate).ToListAsync();
        #endregion
    }
}