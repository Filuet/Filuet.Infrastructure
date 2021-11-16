using Filuet.Infrastructure.Abstractions.DataProvider;
using Filuet.Infrastructure.DataProvider.Interfaces;

namespace Filuet.Infrastructure.DataProvider.Entities
{
    public abstract class EntityDeletable<T> : Entity<T>, IDeletable
    {
        public virtual bool Deleted { get; protected set; } = false;

        public void MarkDeleted()
        {
            Deleted = true;
            ChangeState(DbState.Modified);
        }

        public virtual void Restore()
        {
            Deleted = false;
            ChangeState(DbState.Modified);
        }

        private void ChangeState(DbState state)
        { ((IDbState)this).ChangeDbState(state); }
    }
}
