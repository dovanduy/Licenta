namespace Licenta.Messaging.Messages.Events
{
    public interface IPackageDeliveredEvent
    {
        int PackageId { get; set; }
    }
}