namespace Licenta.Messaging.Messages
{
    public interface IProductPriceUpdatedEvent
    {
        int ProductId { get; set; }
        decimal Price { get; set; }
    }
}
