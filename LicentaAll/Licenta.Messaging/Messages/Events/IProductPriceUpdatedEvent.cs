namespace Licenta.Messaging.Messages.Events
{
    public interface IProductPriceUpdatedEvent
    {
        int ProductId { get; set; }
        decimal Price { get; set; }
    }
}
