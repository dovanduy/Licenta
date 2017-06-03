using System.Threading.Tasks;
using Licenta.Messaging.Messages.Commands;
using Licenta.Messaging.Model;
using LicentaHighLevelApi.Model.Messages;
using LicentaHighLevelApi.Services.Interfaces;

namespace LicentaHighLevelApi.Services
{
    public class ReviewService : IReviewService
    {
        public async Task Add(Review review)
        {
            using (BusMessagingService busService = new BusMessagingService())
            {
                await busService.BusControl.Publish<IAddReviewCommand>(new AddReviewCommand {Review = review});
            }
        }

        public async Task Update(Review review)
        {
            using (BusMessagingService busService = new BusMessagingService())
            {
                await busService.BusControl.Publish<IUpdateReviewCommand>(new UpdateReviewCommand {Review = review});
            }
        }

        public async Task Delete(Review review)
        {
            await Delete(review.ProductId);
        }

        public async Task Delete(int reviewId)
        {
            using (BusMessagingService busService = new BusMessagingService())
            {
                await busService.BusControl.Publish<IDeleteReviewCommand>(new DeleteReviewCommand {ReviewId = reviewId});
            }
        }
    }
}