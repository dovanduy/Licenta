using System;

namespace Licenta.Messaging.Messages.Events
{
    public interface IOrderRegisteredEvent
    {
        Guid CorrelationId { get; set; }
    }
}