namespace Licenta.Messaging.Messages.Events
{
    public interface IProductDeletedEvent
    {
        int ProductId { get; set; }
    }
}
