using Licenta.Messaging.Messages.Events;
using Licenta.ProductView.EntityFramework;
using MassTransit;
using System;
using System.Data.Entity.Core;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.ProductView.Consumers
{
    public class ProductPriceUpdatedEventConsumer : IConsumer<IProductPriceUpdatedEvent>
    {
        public async Task Consume(ConsumeContext<IProductPriceUpdatedEvent> context)
        {
            using (ProductViewDbContext unitOfWork = new ProductViewDbContext())
            {
                var updatedProductId = context.Message.ProductId;
                var updatedProductPrice = context.Message.Price;

                if (unitOfWork.Products.Any(x => x.ProductId == updatedProductId))
                {
                    var productToUpdate = unitOfWork.Products.First(x => x.ProductId == updatedProductId);
                    unitOfWork.Products.Attach(productToUpdate);
                    productToUpdate.Price = updatedProductPrice;

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
