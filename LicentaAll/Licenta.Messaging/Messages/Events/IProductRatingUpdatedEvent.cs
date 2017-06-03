namespace Licenta.Messaging.Messages.Events
{
    public interface IProductRatingUpdatedEvent
    {
        int ProductId { get; set; }
        double Rating { get; set; }
    }
}
