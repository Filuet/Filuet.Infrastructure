using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Filuet.Infrastructure.DataProvider.Interfaces.Repositories
{
    public interface IRepositoryAsync<T> : IRepository
        where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetAsync(object id);


        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);
    }
}
