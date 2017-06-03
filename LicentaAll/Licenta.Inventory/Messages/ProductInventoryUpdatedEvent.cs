using Licenta.Messaging.Messages.Events;

namespace Licenta.Inventory.Messages
{
    public class ProductInventoryUpdatedEvent : IProductInventoryUpdatedEvent
    {
        public int ProductId { get; set; }
        public int Inventory { get; set; }
    }
}