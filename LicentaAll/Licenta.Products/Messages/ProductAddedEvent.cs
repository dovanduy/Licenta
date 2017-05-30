using Licenta.Messaging.Messages;

namespace Licenta.Products.Messages
{
    public class ProductAddedEvent : IProductAddedEvent
    {
        public Messaging.Model.Product Product { get; set; }
    }
}
