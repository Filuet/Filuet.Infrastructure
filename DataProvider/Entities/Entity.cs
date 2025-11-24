using Filuet.Infrastructure.Abstractions.DataProvider;
using Filuet.Infrastructure.DataProvider.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Filuet.Infrastructure.DataProvider.Entities
{
    public abstract class Entity<T> : IdentifiableEntity<T>, IEntity, IDbState
    {
        protected DbState _dbState = DbState.None;

        DbState IDbState.DbState => _dbState;

        void IDbState.ChangeDbState(DbState state)
        { _dbState = state; }

        void IDbState.DeleteFromDb()
        { _dbState = DbState.Deleted; }

        /// <summary>
        /// Sorting index
        /// </summary>
        [NotMapped]
        public virtual int Index { get; set; } = 0;
    }
}