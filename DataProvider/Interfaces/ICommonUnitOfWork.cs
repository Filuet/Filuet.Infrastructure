using Filuet.Infrastructure.DataProvider.Interfaces.Repositories;
using System;

namespace Filuet.Infrastructure.DataProvider.Interfaces
{
    public interface ICommonUnitOfWork : IDisposable {
        T GetRepository<T>() where T : IRepository;

        void SaveChanges();

        void TransactionBegin(System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.Serializable);

        void TransactionCommit();

        void TransactionRollback();
    }
}