using Licenta.Messaging.Messages.Commands;

namespace LicentaHighLevelApi.Model.Messages
{
    public class DeleteCategoryCommand : IDeleteCategoryCommand
    {
        public int CategoryId { get; set; }
    }
}