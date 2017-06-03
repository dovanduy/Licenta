using Licenta.Messaging.Messages.Events;

namespace Licenta.Products.Messages
{
    public class ProductDeletedEvent : IProductDeletedEvent
    {
        public int ProductId { get;set; }
    }
}
