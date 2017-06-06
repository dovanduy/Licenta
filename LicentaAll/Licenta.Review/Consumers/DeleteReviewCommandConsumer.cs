using System;
using System.Data.Entity.Core;
using System.Linq;
using System.Threading.Tasks;
using Licenta.Messaging.Messages.Commands;
using Licenta.Messaging.Messages.Events;
using Licenta.Review.EntityFramework;
using Licenta.Review.Messages;
using MassTransit;

namespace Licenta.Review.Consumers
{
    class DeleteReviewCommandConsumer : IConsumer<IDeleteReviewCommand>
    {
        public async Task Consume(ConsumeContext<IDeleteReviewCommand> context)
        {
            using (ReviewDbContext unitOfWork = new ReviewDbContext())
            {
                var reviewToBeDeleted = context.Message.ReviewId;
                if (unitOfWork.Reviews.Any(x => x.ReviewId == reviewToBeDeleted))
                {
                    var deletedReview = unitOfWork.Reviews.First(x => x.ReviewId == reviewToBeDeleted);

                    unitOfWork.Reviews.Attach(deletedReview);
                    deletedReview.DeletionDate = DateTime.Now;

                    await unitOfWork.SaveChangesAsync();
                    await Console.Out.WriteLineAsync($"Review {deletedReview.ReviewId} was deleted.");

                    await context.Publish(CreateProductRatingUpdatedEvent(unitOfWork, deletedReview.ProductId));
                }
                else
                {
                    throw new EntityCommandExecutionException($"Review {reviewToBeDeleted} does not exist.");
                }
            }


        }

        private IProductRatingUpdatedEvent CreateProductRatingUpdatedEvent(IReviewDbContext unitOfWork, int productId)
        {
            double rating = unitOfWork.ProductRatings.First(x => x.ProductId == productId).AverageRating.Value;
            return new ProductRatingUpdatedEvent
            {
                ProductId = productId,
                Rating = rating
            };
        }
    }
}
