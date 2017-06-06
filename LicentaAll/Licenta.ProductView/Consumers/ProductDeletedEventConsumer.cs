using System;
using System.Threading.Tasks;
using Licenta.Messaging.Messages.Events;
using MassTransit;
using Licenta.ProductView.EntityFramework;
using System.Linq;
using System.Data.Entity.Core;
using System.Data.Entity;

namespace Licenta.ProductView.Consumers 
{
    class ProductDeletedEventConsumer : IConsumer<IProductDeletedEvent>
    {
        public async Task Consume(ConsumeContext<IProductDeletedEvent> context)
        {
            using (ProductViewDbContext unitOfWork = new ProductViewDbContext())
            {
                var idOfProductToBeDeleted = context.Message.ProductId;

                if (unitOfWork.Products.Any(x => x.ProductId == idOfProductToBeDeleted))
                {
                    var editedProduct = unitOfWork.Products
                        .Where(x => x.ProductId == idOfProductToBeDeleted)
                        .Include(x => x.AditionalDetails)
                        .ToList().First();

                    unitOfWork.Products.Remove(editedProduct);

                    await unitOfWork.SaveChangesAsync();
                    await Console.Out.WriteLineAsync($"Product {idOfProductToBeDeleted} was updated.");
                }
                else
                {
                    throw new EntityCommandExecutionException($"No product with id {idOfProductToBeDeleted}");
                }
            }
        }
    }
}
