using System;
using System.Data.Entity.Core;
using System.Linq;
using System.Threading.Tasks;
using Licenta.Messaging.Messages.Commands;
using Licenta.Messaging.Messages.Events;
using Licenta.Review.EntityFramework;
using Licenta.Review.Messages;
using Licenta.Review.Services.Interfaces;
using MassTransit;

namespace Licenta.Review.Consumers
{
    class DeleteReviewCommandConsumer : IConsumer<IDeleteReviewCommand>
    {
        private IReviewService ReviewService;

        public DeleteReviewCommandConsumer(IReviewService reviewService)
        {
            ReviewService = reviewService;
        }

        public async Task Consume(ConsumeContext<IDeleteReviewCommand> context)
        {
            var deletedReviewProductId = await ReviewService.DeleteReview(context.Message.ReviewId);

            await Console.Out.WriteLineAsync($"Review {context.Message.ReviewId} was deleted.");

            await context.Publish(
                CreateProductRatingUpdatedEvent(ReviewService.GetProductRating(deletedReviewProductId),
                                                 deletedReviewProductId));
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
