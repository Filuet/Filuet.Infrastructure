using Filuet.Infrastructure.DataProvider.Entities;
using Filuet.Infrastructure.DataProvider.Interfaces.Repositories;

namespace Filuet.Utils.Abstractions.Platform.DataInfrastructure.Interfaces.Repositories
{
    public interface IMemoryRepository<TEntity, TKey> : IRepository<TEntity>
        where TEntity : Entity<TKey> { }
}