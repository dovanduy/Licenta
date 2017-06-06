using System;
using System.Data.Entity.Core;
using System.Linq;
using System.Threading.Tasks;
using Licenta.Messaging.Messages.Events;
using Licenta.ProductView.EntityFramework;
using MassTransit;

namespace Licenta.ProductView.Consumers
{
    public class ProductRatingUpdatedEventConsumer : IConsumer<IProductRatingUpdatedEvent> 
    {
        public async Task Consume(ConsumeContext<IProductRatingUpdatedEvent> context)
        {
            using (ProductViewDbContext unitOfWork = new ProductViewDbContext())
            {
                var updatedProductId = context.Message.ProductId;

                if (unitOfWork.Products.Any(x => x.ProductId == updatedProductId))
                {
                    var productToEdit = unitOfWork.Products.First(x => x.ProductId == updatedProductId);
                    unitOfWork.Products.Attach(productToEdit);
                    productToEdit.Rating = Convert.ToDecimal(context.Message.Rating);

                    await unitOfWork.SaveChangesAsync();
                    await Console.Out.WriteLineAsync($"Product {updatedProductId} price was changed.");
                }
                else
                {
                    throw new EntityCommandExecutionException($"No product with id {updatedProductId}");
                }
            }
        }
    }
}
