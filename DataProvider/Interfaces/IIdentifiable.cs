namespace Filuet.Infrastructure.DataProvider.Interfaces
{
    public interface IIdentifiable<T>
    {
        T ID { get; }
    }
}