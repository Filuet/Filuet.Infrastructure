namespace Filuet.Infrastructure.Abstractions.Models
{
    /// <summary>
    /// Event broker between IEventProducer and IEventConsumer
    /// </summary>
    public class EventBroker : IEventBroker
    {
        private readonly IEventConsumer _consumer;

        /// <summary>
        /// Constructor to set IEventConsumer
        /// </summary>
        /// <param name="consumer">IEventConsumer implementation</param>
        public EventBroker(IEventConsumer consumer)
        {
            _consumer = consumer;
        }

        /// <summary>
        /// (renamed SetProducer) Append IEventProducer whose OnEvent will be consumed by IEventConsumer
        /// </summary>
        /// <param name="producer">IEventProducer implementation</param>
        public void AppendProducer(IEventProducer producer)
        {
            producer.OnEvent += _consumer.Consume;
        }
    }
}