using Licenta.Messaging.Messages.Events;

namespace Licenta.Products.Messages
{
    public class ProductUpdatedEvent : IProductUpdatedEvent
    {
        public Messaging.Model.Product Product { get; set; }
    }
}
