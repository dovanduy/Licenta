using Licenta.Messaging.Model;

namespace Licenta.Messaging.Messages.Commands
{
    public interface IAddProductCommand
    {
        Product Product { get; set; }
    }
}
