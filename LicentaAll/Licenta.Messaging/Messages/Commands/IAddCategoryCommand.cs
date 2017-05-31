using Licenta.Messaging.Model;

namespace Licenta.Messaging.Messages.Commands
{
    public interface IAddCategoryCommand
    {
        Category Category { get; set; }
    }
}
