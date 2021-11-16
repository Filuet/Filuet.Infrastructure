namespace Filuet.Infrastructure.Abstractions.DataProvider
{
    public enum DbState
    {
        None,
        Unchanged,
        Added,
        Modified,
        Deleted,
        Detached
    }
}