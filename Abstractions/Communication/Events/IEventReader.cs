using System;
using System.Collections.Generic;

namespace Filuet.Infrastructure.Abstractions.Models
{
    /// <summary>
    /// Read log from storage
    /// </summary>
    public interface IEventReader
    {
        IEnumerable<EventItem> GetEventsAsync(DateTimeOffset from, int? first = 150);

        IEnumerable<EventItem> GetEventsAsync(DateTimeOffset from, DateTimeOffset till);
    }
}