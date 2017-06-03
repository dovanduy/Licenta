using Licenta.Messaging.Messages.Events;

namespace Licenta.Sales.Messages
{
    public class ProductPriceUpdatedEvent : IProductPriceUpdatedEvent
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }
    }
}
