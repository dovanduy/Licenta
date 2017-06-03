using Licenta.Messaging.Messages.Commands;
using Licenta.Messaging.Model;

namespace LicentaHighLevelApi.Model.Messages
{
    public class AddReviewCommand : IAddReviewCommand
    {
        public Review Review { get; set; }
    }
}