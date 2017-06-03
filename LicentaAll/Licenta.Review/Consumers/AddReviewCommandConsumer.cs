using System;
using System.Data.Entity.Core;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Licenta.Messaging.Messages.Commands;
using Licenta.Messaging.Messages.Events;
using Licenta.Review.EntityFramework;
using Licenta.Review.Messages;

namespace Licenta.Review.Consumers
{
    public class AddReviewCommandConsumer : IConsumer<IAddReviewCommand>
    {
        public async Task Consume(ConsumeContext<IAddReviewCommand> context)
        {
            using (ReviewDbContext unitOfWork = new ReviewDbContext())
            {
                await Console.Out.WriteLineAsync("Review add command recieved.");
                var reviewToBeAdded = context.Message.Review;

                bool userAlreadyReaviewed =
                    unitOfWork.Reviews.Any(x => x.UserId == reviewToBeAdded.UserId &&
                                                x.ProductId == reviewToBeAdded.ProductId &&
                                                !x.DeletionDate.HasValue);

                if (!userAlreadyReaviewed)
                {
                    var addedReview = new EntityFramework.Review
                    {
                        ProductId = reviewToBeAdded.ProductId,
                        Rating = reviewToBeAdded.Rating,
                        UserId = reviewToBeAdded.UserId,
                        Text = reviewToBeAdded.Text,
                        UserNickname = reviewToBeAdded.UserNickname,
                        UserBoughtProduct = reviewToBeAdded.UserBoughtProduct
                    };

                    unitOfWork.Reviews.Add(addedReview);
                    await unitOfWork.SaveChangesAsync();
                    await Console.Out.WriteLineAsync($"Review {addedReview.ReviewId} was added.");

                    await context.Publish(CreateProductRatingUpdatedEvent(unitOfWork, addedReview.ProductId));

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    await Console.Out.WriteLineAsync("Event published: ProductRatingUpdatedEvent");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    string message = $"User {reviewToBeAdded.UserId} already reviewed product {reviewToBeAdded.ProductId}.";
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
