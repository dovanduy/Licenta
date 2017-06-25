namespace Licenta.Messaging.Messages.Events
{
    public interface IPackagePickedUpEvent
    {
        int OrderId { get; set; }
        int PackageId { get; set; }
    }
}