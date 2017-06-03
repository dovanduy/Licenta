using Licenta.Messaging.Model;

namespace Licenta.Messaging.Messages.Commands
{
    public interface IAddReviewCommand
    {
        Review Review { get; set; }
    }
}
