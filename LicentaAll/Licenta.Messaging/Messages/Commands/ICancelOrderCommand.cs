namespace Licenta.Messaging.Messages.Commands
{
    public interface ICancelOrderCommand
    {
        int OrderId { get; set; }
    }
}