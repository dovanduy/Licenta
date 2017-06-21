using System;
using System.Linq;
using System.Threading.Tasks;
using Licenta.Messaging.Messages.Events;
using Licenta.Review.Services.Interfaces;
using MassTransit;

namespace Licenta.Review.Consumers
{
    public class ProductDeletedEventConsumer : IConsumer<IProductDeletedEvent>
    {
        private IReviewService ReviewService;

        public ProductDeletedEventConsumer(IReviewService reviewService)
        {
            ReviewService = reviewService;
        }

        public async Task Consume(ConsumeContext<IProductDeletedEvent> context)
        {
            var idsOfChangedReviews = await ReviewService.HandleProductDeleted(context.Message.ProductId);
            if (idsOfChangedReviews.Any())
            {
                await Console.Out.WriteLineAsync(
                    $"Reviews that were affected by product deletion: {string.Join(",", idsOfChangedReviews)}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                await Console.Out.WriteLineAsync("No reviews need to be changed.");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }
}
