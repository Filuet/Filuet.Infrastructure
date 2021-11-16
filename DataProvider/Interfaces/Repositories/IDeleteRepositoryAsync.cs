using System.Threading.Tasks;

namespace Filuet.Infrastructure.DataProvider.Interfaces.Repositories
{
    public interface IDeletableRepositoryAsync<T> : IRepositoryAsync<T>
        where T : class
    {
        Task DeleteAsync(T entity);
    }
}
