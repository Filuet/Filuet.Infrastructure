namespace Filuet.Infrastructure.Abstractions.Models
{
    public interface IEventWriter
    {
        void Push(EventItem item);
    }
}