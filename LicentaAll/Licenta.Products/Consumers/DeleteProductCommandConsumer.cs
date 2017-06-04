using Licenta.Messaging.Messages.Commands;
using Licenta.Messaging.Messages.Events;
using Licenta.Products.Messages;
using MassTransit;
using System;
using System.Data.Entity.Core;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Products.Consumers
{
    public class DeleteProductCommandConsumer : IConsumer<IDeleteProductCommand>
    {
        public async Task Consume(ConsumeContext<IDeleteProductCommand> context)
        {
            using (ProductsDbContext unitOfWork = new ProductsDbContext())
            {
                var idOfProductToBeDeleted = context.Message.ProductId;

                await Console.Out.WriteLineAsync($"Product delete command recieved for product {idOfProductToBeDeleted}.");

                if (unitOfWork.Products.Any(x => x.ProductId == idOfProductToBeDeleted))
                {
                    var editedProduct = unitOfWork.Products.First(x => x.ProductId == idOfProductToBeDeleted);
                    unitOfWork.Products.Attach(editedProduct);
                    editedProduct.DateDeleted = DateTime.Now;

                    /*
                    var editedProduct = unitOfWork.Products
                        .Where(x => x.ProductId == idOfProductToBeDeleted)
                        .Include(x => x.AditionalDetails)
                        .ToList().First();

                    unitOfWork.Products.Remove(editedProduct);
                    */

                    await unitOfWork.SaveChangesAsync();
                    await Console.Out.WriteLineAsync($"Product {idOfProductToBeDeleted} was deleted.");

                    await context.Publish(CreateProductDeletedEvent(idOfProductToBeDeleted));

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    await Console.Out.WriteLineAsync("Event published: ProductDeletedEvent");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    await Console.Out.WriteLineAsync($"No product with id {idOfProductToBeDeleted}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    throw new EntityCommandExecutionException($"No product with id {idOfProductToBeDeleted}");
                }
            }
        }

        private IProductDeletedEvent CreateProductDeletedEvent(int productId)
        {
            return new ProductDeletedEvent { ProductId = productId };
        }
    }
}
