using  System;

namespace Filuet.Infrastructure.DataProvider.Interfaces.Repositories
{
    public interface IDeletableRepository<T> : IRepository<T>
        where T : class
    {
        void Delete(T entity);
    }
}
