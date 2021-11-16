using System;

namespace Filuet.Infrastructure.Abstractions.Models
{
    public interface IEventProducer
    {
        event EventHandler<EventItem> OnEvent;
    }
}