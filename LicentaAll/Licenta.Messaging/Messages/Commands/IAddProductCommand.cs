using Licenta.Messaging.Model;

namespace Licenta.Messaging.Messages
{
    public interface IAddProductCommand
    {
        Product Product { get; set; }
    }
}
