using Licenta.Messaging.Messages.Commands;

namespace LicentaHighLevelApi.Model.Messages
{
    public class DeleteReviewCommand : IDeleteReviewCommand
    {
        public int ReviewId { get; set; }
    }
}