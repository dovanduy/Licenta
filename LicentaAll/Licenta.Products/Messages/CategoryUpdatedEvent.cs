using Licenta.Messaging.Messages.Events;

namespace Licenta.Products.Messages
{
    public class CategoryUpdatedEvent : ICategoryUpdatedEvent
    {
        public Messaging.Model.Category Category {get;set;}
    }
}
