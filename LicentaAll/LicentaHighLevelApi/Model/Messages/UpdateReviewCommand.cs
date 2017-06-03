using Licenta.Messaging.Messages.Commands;
using Licenta.Messaging.Model;

namespace LicentaHighLevelApi.Model.Messages
{
    public class UpdateReviewCommand: IUpdateReviewCommand
    {
        public Review Review { get; set; }
    }
}