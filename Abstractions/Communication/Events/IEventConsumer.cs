namespace Filuet.Infrastructure.Abstractions.Models
{
    public interface IEventConsumer
    {
        void Consume(object sender, EventItem item);

        IEventConsumer AppendWriter<T>(IEventWriter eventWriter);
    }
}