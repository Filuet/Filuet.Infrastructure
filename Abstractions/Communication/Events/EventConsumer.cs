using System;
using System.Collections.Generic;
using System.Linq;

namespace Filuet.Infrastructure.Abstractions.Models
{
    /// <summary>
    /// Consumer that writes events via IEventWriter
    /// </summary>
    public class EventConsumer : IEventConsumer
    {
        private Dictionary<IEventWriter, Type> _eventWriters = new Dictionary<IEventWriter, Type>();

        /// <summary>
        /// Constructor to inject IEventWriter
        /// </summary>
        public EventConsumer()
        { }

        public IEventConsumer AppendWriter<T>(IEventWriter eventWriter)
        {
            if (eventWriter != null)
                _eventWriters.Add(eventWriter, typeof(T));

            return this;
        }

        /// <summary>
        /// Push event to IEventWriter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="item"></param>
        public void Consume(object sender, EventItem item)
        {
            foreach (var ew in _eventWriters.Keys)
            {
                ew.Push(item);
            }
        }
    }
}