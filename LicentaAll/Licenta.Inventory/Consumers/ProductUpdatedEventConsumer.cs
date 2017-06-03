using System;
using System.Linq;
using System.Threading.Tasks;
using Licenta.Inventory.Messages;
using Licenta.Messaging.Messages.Events;
using MassTransit;

namespace Licenta.Inventory.Consumers
{
    class ProductUpdatedEventConsumer : IConsumer<IProductUpdatedEvent>
    {
        public async Task Consume(ConsumeContext<IProductUpdatedEvent> context)
        {
            using (InventoryDbContext unitOfWork = new InventoryDbContext())
            {
                var updatedProductId = context.Message.Product.ProductId;
                await Console.Out.WriteLineAsync($"Product updated event recieved for product {updatedProductId}.");
                var inventory = context.Message.Product.Inventory;

                if (unitOfWork.Products.Any(x => x.ProductId == updatedProductId))
                {
                    var productToUpdate = unitOfWork.Products.First(x => x.ProductId == updatedProductId);
                    unitOfWork.Products.Attach(productToUpdate);
                    productToUpdate.Items = inventory;

                    await unitOfWork.SaveChangesAsync();
                    await Console.Out.WriteLineAsync($"Product {updatedProductId} number of items changed.");
                }
                else
                {
                    unitOfWork.Products.Add(new Product
                    {
                        ProductId = updatedProductId,
                        Items = inventory
                    });

                    await unitOfWork.SaveChangesAsync();
                    await Console.Out.WriteLineAsync($"Product {updatedProductId} added.");
                }

                await context.Publish(CreateInventoryUpdatedEvent(updatedProductId,inventory));
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                await Console.Out.WriteLineAsync("Event published: ProductInventoryUpdatedEvent");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        private IProductInventoryUpdatedEvent CreateInventoryUpdatedEvent(int productId, int inventory)
        {
            return new ProductInventoryUpdatedEvent
            {
                ProductId = productId,
                Inventory = inventory
            };
        }
    }
}
