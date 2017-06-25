namespace Licenta.Messaging.Messages.Events
{
    public interface IOrderNeedsConfirmationEvent
    {
        int OrderId { get; set; }
    }
}