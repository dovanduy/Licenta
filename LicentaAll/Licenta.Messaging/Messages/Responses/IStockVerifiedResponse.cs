using System;

namespace Licenta.Messaging.Messages.Responses
{
    public interface IStockVerifiedResponse
    {
        int OrderId { get; set; }
        Guid CorrelationId { get; set; }
    }
}