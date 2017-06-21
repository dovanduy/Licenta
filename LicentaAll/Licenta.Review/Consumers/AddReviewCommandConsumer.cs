using System;
using System.Threading.Tasks;
using MassTransit;
using Licenta.Messaging.Messages.Commands;
using Licenta.Messaging.Messages.Events;
using Licenta.Review.Messages;
using Licenta.Review.Services.Interfaces;

namespace Licenta.Review.Consumers
{
    public class AddReviewCommandConsumer : IConsumer<IAddReviewCommand>
    {
        private IReviewService ReviewService;

        public AddReviewCommandConsumer(IReviewService reviewService)
        {
            ReviewService = reviewService;
        }

        public async Task Consume(ConsumeContext<IAddReviewCommand> context)
        {
            var addedReview = await ReviewService.AddNewReview(context.Message.Review);
            await Console.Out.WriteLineAsync($"Review {addedReview.Id} was added.");
            await context.Publish(CreateProductRatingUpdatedEvent(ReviewService.GetProductRating(addedReview.ProductId), addedReview.ProductId));
        }

        private IProductRatingUpdatedEvent CreateProductRatingUpdatedEvent(double rating, int productId)
        {
            return new ProductRatingUpdatedEvent
            {
                ProductId = productId,
                Rating = rating
            };
        }
    }
}
