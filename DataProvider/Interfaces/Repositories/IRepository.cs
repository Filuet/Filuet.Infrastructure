using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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

        T Add(T entity);

        T Update(T entity);

        T Modify(T entity);

        void Delete(T entity);

        void Restore(T entity);

        void Refresh(T entity);
    }
}