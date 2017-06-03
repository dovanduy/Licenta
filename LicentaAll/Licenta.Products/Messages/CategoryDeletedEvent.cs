using Licenta.Messaging.Messages.Events;

namespace Licenta.Products.Messages
{
    public class CategoryDeletedEvent : ICategoryDeletedEvent
    {
        public int CategoryId { get; set; }
    }
}
