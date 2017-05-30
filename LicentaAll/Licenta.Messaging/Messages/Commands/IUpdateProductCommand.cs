using Licenta.Messaging.Model;

namespace Licenta.Messaging.Messages.Commands
{
    public interface IUpdateProductCommand
    {
        Product Product { get; set; }
    }
}
