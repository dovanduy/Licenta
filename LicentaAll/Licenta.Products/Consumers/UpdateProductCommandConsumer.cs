﻿using Licenta.Messaging.Messages.Commands;
using Licenta.Messaging.Messages.Events;
using Licenta.Products.Enumerations;
using Licenta.Products.Messages;
using MassTransit;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Products.Consumers
{
    class UpdateProductCommandConsumer : IConsumer<IUpdateProductCommand>
    {
        public async Task Consume(ConsumeContext<IUpdateProductCommand> context)
        {
            using (ProductsDbContext unitOfWork = new ProductsDbContext())
            {
                var productToEdit = context.Message.Product;

                var aditionalDetails = productToEdit.AditionalDetails.Select(x => new AditionalDetail
                {
                    AditionalDetailId = x.AditionalDetailId,
                    Name = x.Name,
                    Text = x.Text
                }).ToList();

                var editedProduct = unitOfWork.Products.First(x => x.ProductId == productToEdit.ProductId);

                unitOfWork.Products.Attach(editedProduct);

                editedProduct.Name = productToEdit.Name;
                editedProduct.Description = productToEdit.Description;
                editedProduct.CategoryId = (CategoryEnum) productToEdit.CategoryId;
                editedProduct.AditionalDetails = aditionalDetails;

                await unitOfWork.SaveChangesAsync();
                await Console.Out.WriteLineAsync($"Product {editedProduct.ProductId} was updated.");

                await context.Publish(MapProductUpdatedToMessaje(editedProduct));

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                await Console.Out.WriteLineAsync("Event published: ProductUpdatedEvent");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private IProductUpdatedEvent MapProductUpdatedToMessaje(Product addedProduct)
        {
            return new ProductUpdatedEvent
            {
                Product = new Messaging.Model.Product
                {
                    ProductId = addedProduct.ProductId,
                    CategoryId = (int)addedProduct.CategoryId,
                    Description = addedProduct.Description,
                    Name = addedProduct.Name,
                    AditionalDetails = addedProduct.AditionalDetails.Select(x => new Messaging.Model.AditionalDetail
                    {
                        AditionalDetailId = x.AditionalDetailId,
                        Name = x.Name,
                        Text = x.Text
                    }).ToList()
                }
            };
        }
    }
}