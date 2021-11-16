using Filuet.Infrastructure.DataProvider.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Filuet.Utils.Abstractions.DataProvider
{
    public class BaseRepository<TDbContext, TEntity, TKey> : CommonRepository<TDbContext, TEntity, TKey>
            where TDbContext : DbContext
            where TEntity : Entity<TKey>
    {
        protected virtual IQueryable<TEntity> GetQuery(bool multiple, bool tracking = false)
        {
            IQueryable<TEntity> query = this.DbSet;

            if (!tracking)
                query = query.AsNoTracking();

            return query;
        }

        protected override IQueryable<TEntity> QueryTracking
        { get { return this.GetQuery(false, true); } }

        protected override IQueryable<TEntity> QueryUntracking
        => this.GetQuery(true, false);

        public BaseRepository(TDbContext context)
            : base(context) { IsAutoSave = true; }

        public virtual TEntity GetTracking(TKey id)
        {
            var entity = base.GetEntityByKey(QueryTracking, id);
            base.DbContext.Entry(entity).State = EntityState.Detached;
            this.Refresh(entity);

            return entity;
        }

        public virtual TEntity GetUntracking(TKey id)
        {
            var entity = base.GetEntityByKey(QueryUntracking, id);
            base.DbContext.Entry(entity).State = EntityState.Detached;
            this.Refresh(entity);

            return entity;
        }
    }

    public class BaseModelRepository<TDbContext, TEntity, TKey> : BaseRepository<TDbContext, TEntity, TKey>
         where TDbContext : DbContext, new()
         where TEntity : Entity<TKey>
    {
        protected override IQueryable<TEntity> QueryTracking
        { get { return this.GetQuery(false, false); } }

        protected override IQueryable<TEntity> QueryUntracking
        { get { return this.GetQuery(true, false); } }

        public BaseModelRepository(TDbContext context)
            : base(context) { }
    }
}
