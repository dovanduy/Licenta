using Licenta.Messaging.Messages;

namespace LicentaHighLevelApi.Model.Messages
{
    public class AddProductCommand : IAddProductCommand
    {
        public Licenta.Messaging.Model.Product Product { get; set; }
    }
}