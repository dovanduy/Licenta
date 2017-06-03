using Licenta.Messaging.Model;

namespace Licenta.Messaging.Messages.Commands
{
    public interface IUpdateReviewCommand
    {
        Review Review { get; set; }
    }
}