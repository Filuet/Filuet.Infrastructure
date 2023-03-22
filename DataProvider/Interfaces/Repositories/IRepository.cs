using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Filuet.Infrastructure.DataProvider.Interfaces.Repositories
{
    public interface IRepository { }

    public interface IRepository<T> : IRepository
        where T : class
    {
        IEnumerable<T> GetAll(bool tracking = false);

        Task<IEnumerable<T>> GetAllAsync(bool tracking = false);

        T Get(object id, bool tracking = true);

        IEnumerable<T> Add(IEnumerable<T> entities);

        IEnumerable<T> Get(Expression<Func<T, bool>> predicate = null, bool tracking = false);

        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate = null, bool tracking = false);

        T Add(T entity);

        Task<T> AddAsync(T entity);

        T Update(T entity);
        Task<T> UpdateAsync(T entity);

        Task<ICollection<T>> UpdateScopeAsync(ICollection<T> entities);

        T Modify(T entity);

        void Delete(T entity);

        Task DeleteAsync(T entity);

        Task DeleteAsync(ICollection<T> entities);

        void Restore(T entity);

        void Refresh(T entity);
    }
}