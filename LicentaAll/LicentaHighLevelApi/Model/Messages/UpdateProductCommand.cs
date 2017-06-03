using Licenta.Messaging.Messages.Commands;
using Licenta.Messaging.Model;

namespace LicentaHighLevelApi.Model.Messages
{
    public class UpdateProductCommand : IUpdateProductCommand
    {
        public Product Product { get; set; }
    }
}