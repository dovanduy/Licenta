namespace Licenta.Messaging.Messages.Responses
{
    public interface IDeliveryDateAproximationResponse
    {
        string CorrelationId { get; set; }
        int OrderId { get; set; }
    }
}