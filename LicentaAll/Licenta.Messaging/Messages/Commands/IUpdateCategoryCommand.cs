using Licenta.Messaging.Model;

namespace Licenta.Messaging.Messages.Commands
{
    public interface IUpdateCategoryCommand
    {
        Category Category { get; set; }
    }
}
