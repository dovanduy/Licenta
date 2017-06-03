using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licenta.Messaging.Messages.Events;
using Licenta.Review.EntityFramework;
using MassTransit;

namespace Licenta.Review.Consumers
{
    public class ProductDeletedEventConsumer : IConsumer<IProductDeletedEvent>
    {
        public async Task Consume(ConsumeContext<IProductDeletedEvent> context)
        {
            using (ReviewDbContext unitOfWork = new ReviewDbContext())
            {
                var productId = context.Message.ProductId;
                await Console.Out.WriteLineAsync($"Product deleted event recieved for {productId}.");
                if (unitOfWork.Reviews.Any(x => x.ProductId == productId && !x.DeletionDate.HasValue))
                {
                    var reviewsToChange = unitOfWork.Reviews
                        .Where(x => x.ProductId == productId && !x.DeletionDate.HasValue)
                        .ToList();

                    foreach (EntityFramework.Review review in reviewsToChange)
                    {
                        unitOfWork.Reviews.Attach(review);
                        review.ProductDeleted = true;
                    }

                    var idsOfChangedReviews = string.Join(",",reviewsToChange.Select(x => x.ReviewId));
                    await unitOfWork.SaveChangesAsync();
                    await Console.Out.WriteLineAsync($"Reviews that were affected by product deletion: {idsOfChangedReviews}");
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
}
