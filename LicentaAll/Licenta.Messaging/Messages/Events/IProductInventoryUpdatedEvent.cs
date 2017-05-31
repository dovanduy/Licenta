namespace Licenta.Messaging.Messages.Events
{
    public interface IProductInventoryUpdatedEvent
    {
        int ProductId { get; set; }
        int Inventory { get; set; }
    }
}
