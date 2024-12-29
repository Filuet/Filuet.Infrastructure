using System;
using System.Collections.Generic;

namespace Filuet.Infrastructure.Abstractions.Models
{
    public abstract class EventReader : IEventReader
    {
        public IEnumerable<EventItem> GetEventsAsync(DateTimeOffset from, int? first = 150)
            => throw new NotImplementedException();

        public IEnumerable<EventItem> GetEventsAsync(DateTimeOffset from, DateTimeOffset till)
            => throw new NotImplementedException();
    }
}