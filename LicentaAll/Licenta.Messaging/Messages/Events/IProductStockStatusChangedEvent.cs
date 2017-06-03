namespace Licenta.Messaging.Messages.Events
{
    //TODO Do we need this?
    interface IProductStockStatusChangedEvent : IProductInventoryUpdatedEvent
    {
        bool IsInStock { get; set; }
    }
}
