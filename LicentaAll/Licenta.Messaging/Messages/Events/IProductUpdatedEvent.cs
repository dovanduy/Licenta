using Licenta.Messaging.Model;

namespace Licenta.Messaging.Messages.Events
{
    public interface IProductUpdatedEvent
    {
        Product Product { get; set; }
    }
}
