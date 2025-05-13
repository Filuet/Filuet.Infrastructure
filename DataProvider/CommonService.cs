using Filuet.Infrastructure.DataProvider.Interfaces;

namespace Filuet.Infrastructure.DataProvider
{
    public abstract class CommonService<TUnitOfWork> : ICommonService<TUnitOfWork>
           where TUnitOfWork : ICommonUnitOfWork
    {
        protected TUnitOfWork _uow;

        public TUnitOfWork UnitOfWork => _uow;

        public CommonService(TUnitOfWork uow)
            : base() {
            _uow = uow;
        }

        public void SaveChanges()
            => UnitOfWork.SaveChanges();


        public virtual void Dispose()
            => UnitOfWork.Dispose();
    }
}