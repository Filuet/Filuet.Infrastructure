namespace Filuet.Infrastructure.DataProvider.Interfaces
{
    public interface ICommonService<TUnitOfWork>
        where TUnitOfWork : ICommonUnitOfWork
    {
        TUnitOfWork UnitOfWork { get; }
    }
}