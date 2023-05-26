using Filuet.Infrastructure.DataProvider.Entities;
using Filuet.Infrastructure.DataProvider.Interfaces.Repositories;

namespace Filuet.Infrastructure.DataProvider.Interfaces
{
    public interface IMemoryRepository<TEntity, TKey> : IRepository<TEntity>
        where TEntity : Entity<TKey> { }
}