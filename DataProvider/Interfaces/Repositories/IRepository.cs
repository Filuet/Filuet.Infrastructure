using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Filuet.Infrastructure.DataProvider.Interfaces.Repositories
{
    public interface IRepository
    { }

    public interface IRepository<T> : IRepository
        where T : class
    {
        //bool IsAutoSave { get; }

        //int SaveChanges();

        IEnumerable<T> GetAll();

        T Get(object id, bool tracking = true);

        IEnumerable<T> Add(IEnumerable<T> entities);

        IEnumerable<T> Get(Expression<Func<T, bool>> predicate = null);

        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate = null);

        T Add(T entity);

        Task<T> AddAsync(T entity);

        T Update(T entity);

        Task<T> UpdateAsync(T entity);

        T Modify(T entity);

        void Delete(T entity);

        Task DeleteAsync(T entity);

        void Restore(T entity);

        void Refresh(T entity);
    }
}