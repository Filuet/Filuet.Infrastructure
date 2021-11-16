namespace Filuet.Infrastructure.Abstractions.Models
{
    /// <summary>
    /// Event broker interface between IEventProducer and IEventConsumer
    /// IEventConsumer should be injected in constructor implementation
    /// </summary>
    public interface IEventBroker
    {
        /// <summary>
        /// Set IEventProducer whose OnEvent will be consumed by IEventConsumer
        /// </summary>
        /// <param name="producer">IEventProducer implementation</param>
        void AppendProducer(IEventProducer producer);
    }
}