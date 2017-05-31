using Licenta.Messaging.Model;

namespace Licenta.Messaging.Messages.Events
{
    public interface ICategoryUpdatedEvent
    {
        Category Category { get; set; }
    }
}
