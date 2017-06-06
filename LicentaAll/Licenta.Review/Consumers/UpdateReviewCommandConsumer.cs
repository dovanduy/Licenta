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
    public class UpdateReviewCommandConsumer : IConsumer<IUpdateReviewCommand>
    {
        public async Task Consume(ConsumeContext<IUpdateReviewCommand> context)
        {
            using (ReviewDbContext unitOfWork = new ReviewDbContext())
            {
                var reviewToBeEdited = context.Message.Review;
                if (unitOfWork.Reviews.Any(x => x.ReviewId == reviewToBeEdited.ReviewId && !x.DeletionDate.HasValue))
                {
                    bool ratingChanged = false;
                    var editedReview = unitOfWork.Reviews.First(x => x.ReviewId == reviewToBeEdited.ReviewId);

                    unitOfWork.Reviews.Attach(editedReview);

                    if (editedReview.Rating != reviewToBeEdited.Rating)
                    {
                        ratingChanged = true;
                        editedReview.Rating = reviewToBeEdited.Rating;
                    }
                    editedReview.Text = reviewToBeEdited.Text;
                    editedReview.UserNickname = reviewToBeEdited.UserNickname;

                    await unitOfWork.SaveChangesAsync();
                    await Console.Out.WriteLineAsync($"Review {editedReview.ReviewId} was updated.");

                    if (ratingChanged)
                    {
                        await context.Publish(CreateProductRatingUpdatedEvent(unitOfWork, editedReview.ProductId));
                    }
                }
                else
                {
                    throw new EntityCommandExecutionException($"No review with id {reviewToBeEdited.ReviewId}");
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
