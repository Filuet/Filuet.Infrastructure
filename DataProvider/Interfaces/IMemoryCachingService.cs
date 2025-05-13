namespace Filuet.Infrastructure.DataProvider.Interfaces
{
    public interface IMemoryCachingService
    {
        MemoryCacher Get(string cacherName, uint sizeMb = 1);
    }
}