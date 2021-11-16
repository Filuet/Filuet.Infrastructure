namespace Filuet.Infrastructure.DataProvider.Interfaces
{
    public interface IDeletable
    {
        bool Deleted { get; }

        void MarkDeleted();

        void Restore();
    }
}