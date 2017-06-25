using System;

namespace Licenta.Messaging.Messages.Commands
{
    public interface IConfirmOrderCommand
    {
        Guid CorrelationId { get; set; }
        int OrderId { get; set; }
    }
}