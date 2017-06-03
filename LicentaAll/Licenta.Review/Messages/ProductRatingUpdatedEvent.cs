using Licenta.Messaging.Messages.Events;

namespace Licenta.Review.Messages
{
    public class ProductRatingUpdatedEvent : IProductRatingUpdatedEvent
    {
        public int ProductId { get; set; }
        public double Rating { get; set; }
    }
}
