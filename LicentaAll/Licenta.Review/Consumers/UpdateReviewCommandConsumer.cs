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
                await Console.Out.WriteLineAsync($"Review update command recieved for product {reviewToBeEdited.ReviewId}.");
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

                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        await Console.Out.WriteLineAsync("Event published: ProductRatingUpdatedEvent");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
                else
                {
                    string message = $"No review with id {reviewToBeEdited.ReviewId}";
                    Console.ForegroundColor = ConsoleColor.Red;
                    await Console.Out.WriteLineAsync(message);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    throw new EntityCommandExecutionException(message);
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
