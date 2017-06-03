﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licenta.Messaging.Messages.Events;
using Licenta.Sales.Messages;
using MassTransit;

namespace Licenta.Sales.Consumers
{
    class ProductUpdatedEventConsumer : IConsumer<IProductUpdatedEvent>
    {
        public async Task Consume(ConsumeContext<IProductUpdatedEvent> context)
        {
            using (InventoryDbContext unitOfWork = new InventoryDbContext())
            {
                var updatedProductId = context.Message.Product.ProductId;
                await Console.Out.WriteLineAsync($"Product updated event recieved for product {updatedProductId}.");
                var price = context.Message.Product.Price;

                if (unitOfWork.Products.Any(x => x.ProductId == updatedProductId))
                {
                    var productToUpdate = unitOfWork.Products.First(x => x.ProductId == updatedProductId);
                    unitOfWork.Products.Attach(productToUpdate);
                    productToUpdate.Price = price;

                    await unitOfWork.SaveChangesAsync();
                    await Console.Out.WriteLineAsync($"Product {updatedProductId} price changed.");
                }
                else
                {
                    unitOfWork.Products.Add(new Product
                    {
                        ProductId = updatedProductId,
                        Price = price
                    });

                    await unitOfWork.SaveChangesAsync();
                    await Console.Out.WriteLineAsync($"Product {updatedProductId} added.");
                }

                await context.Publish(CreateInventoryUpdatedEvent(updatedProductId, price));
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                await Console.Out.WriteLineAsync("Event published: ProductInventoryUpdatedEvent");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        private IProductPriceUpdatedEvent CreateInventoryUpdatedEvent(int productId, decimal price)
        {
            return new ProductPriceUpdatedEvent
            {
                ProductId = productId,
                Price = price
            };
        }
    }
}