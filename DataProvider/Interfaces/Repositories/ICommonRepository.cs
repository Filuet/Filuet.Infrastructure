using Filuet.Infrastructure.DataProvider.Entities;

namespace Filuet.Infrastructure.DataProvider.Interfaces.Repositories
{
    public interface ICommonRepository<TEntity, T> : IRepository<TEntity>
        where TEntity : Entity<T>
    {
        TEntity GetTracking(T id);
    }

    public interface ICommonRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    { }
}