using Filuet.Infrastructure.Abstractions.DataProvider;

namespace Filuet.Infrastructure.DataProvider.Interfaces
{
    public interface IDbState
    {
        DbState DbState { get; }

        void ChangeDbState(DbState state);

        void DeleteFromDb();
    }
}