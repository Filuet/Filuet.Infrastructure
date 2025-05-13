using Filuet.Infrastructure.DataProvider.Interfaces;
using Filuet.Infrastructure.DataProvider.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;

namespace Filuet.Infrastructure.DataProvider
{
    public class CommonUnitOfWork<TDbContext> : ICommonUnitOfWork where TDbContext : DbContext
    {
        protected Dictionary<Type, IRepository> _repositories;
        private IServiceProvider _provider;

        protected List<IDbContextTransaction> _transactions;

        protected TDbContext _dbContext;

        public TDbContext Context
        {
            get
            {
                if (_dbContext == null)
                    _dbContext = _provider.GetRequiredService<TDbContext>();

                return _dbContext;
            }
        }

        public CommonUnitOfWork(IServiceProvider provider) {
            _repositories = new Dictionary<Type, IRepository>();
            _provider = provider;
        }

        public virtual void SaveChanges()
            => _dbContext.SaveChanges();

        public virtual void Dispose()
            => _dbContext.Dispose();

        public virtual T GetRepository<T>() where T : IRepository
            => _provider.GetRequiredService<T>();

        public void TransactionBegin(IsolationLevel isolationLevel = IsolationLevel.Serializable) {
            if (_transactions != null && _transactions.Count > 0)
                throw new Exception("Unable to duplicate transactions!");

            _transactions = new List<IDbContextTransaction>();

            var transaction = _dbContext.Database.BeginTransaction();
            _transactions.Add(transaction);
        }

        public void TransactionCommit() {
            foreach (var transaction in _transactions) {
                transaction.Commit();
                transaction.Dispose();
            }
            _transactions = null;
        }

        public void TransactionRollback() {
            foreach (var transaction in _transactions) {
                transaction.Rollback();
                transaction.Dispose();
            }
            _transactions = null;
        }
    }
}