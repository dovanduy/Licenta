using Licenta.Messaging.Messages.Commands;

namespace LicentaHighLevelApi.Model.Messages
{
    public class DeleteProductCommand : IDeleteProductCommand
    {
        public int ProductId { get; set; }
    }
}