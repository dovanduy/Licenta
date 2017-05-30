using Licenta.Messaging.Model;

namespace Licenta.Messaging.Messages
{
    public interface IProductAddedEvent
    {
        Product Product { get; set; }
    }
}
