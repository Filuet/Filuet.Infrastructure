using Filuet.Infrastructure.DataProvider.Entities;
using Filuet.Utils.Abstractions.Platform.DataInfrastructure.Interfaces.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Filuet.Infrastructure.DataProvider
{
    public class MemoryRepository<TEntity, TKey> : IMemoryRepository<TEntity, TKey>
           where TEntity : Entity<TKey>
    {
        private static ConcurrentDictionary<TKey, TEntity> _store = new ConcurrentDictionary<TKey, TEntity>();

        public IEnumerable<TEntity> Add(IEnumerable<TEntity> entities)
        {
            List<TEntity> result = new List<TEntity>();
            foreach (var entity in entities)
                result.Add(this.Add(entity));

            return result;
        }

        public TEntity Add(TEntity entity)
            => _store.AddOrUpdate(entity.ID, entity, (k, v) => entity);

        public Task<TEntity> AddAsync(TEntity entity)
            => Task.FromResult(Add(entity));

        public void Delete(TEntity entity)
        {
            _store.TryRemove(entity.ID, out _);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            Delete(entity);
        }

        public TEntity Get(object id, bool tracking = true)
        {
            _store.TryGetValue((TKey)id, out TEntity result);
            return result;
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null)
            => _store.Where(keyVal => predicate == null ? true : predicate.Compile().Invoke(keyVal.Value))
                .Select(x => x.Value);

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null, bool tracking = true)
            => _store.Where(keyVal => predicate == null ? true : predicate.Compile().Invoke(keyVal.Value))
                .Select(x => x.Value);

        public IEnumerable<TEntity> GetAll()
            => _store.Values;

        public IEnumerable<TEntity> GetAll(bool tracking = false)
            => _store.Values;

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = false)
            => await Task.FromResult(_store.Values);

        public Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null)
            => Task.FromResult(Get(predicate));

        public Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null, bool tracking = true)
            => Task.FromResult(Get(predicate, tracking));

        public TEntity Modify(TEntity entity)
            => Add(entity);

        public void Refresh(TEntity entity)
            => Modify(entity);

        public void Restore(TEntity entity)
        {
            Restore(entity);
        }

        public TEntity Update(TEntity entity)
            => Modify(entity);

        public Task<TEntity> UpdateAsync(TEntity entity)
            => Task.FromResult(Update(entity));
    }
}