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
    public class UpdateReviewCommandConsumer : IConsumer<IUpdateReviewCommand>
    {
        private IReviewService ReviewService;

        public UpdateReviewCommandConsumer(IReviewService reviewService)
        {
            ReviewService = reviewService;
        }

        public async Task Consume(ConsumeContext<IUpdateReviewCommand> context)
        {
            double oldRating = ReviewService.GetProductRating(context.Message.Review.ProductId);

            var editedReview = await ReviewService.UpdateReview(context.Message.Review);
            await Console.Out.WriteLineAsync($"Review {editedReview.Id} was updated.");

            double newRating = ReviewService.GetProductRating(editedReview.ProductId);

            if (Math.Abs(oldRating - newRating) >= 0.01)
            {
                await context.Publish(CreateProductRatingUpdatedEvent(newRating, editedReview.ProductId));
            }
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
