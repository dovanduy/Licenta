using Licenta.Messaging.Messages.Commands;
using Licenta.Messaging.Messages.Events;
using Licenta.Products.Messages;
using MassTransit;
using System;
using System.Linq;
using System.Threading.Tasks;
using Licenta.Products.Services.Interfaces;

namespace Licenta.Products.Consumers
{
    public class UpdateProductCommandConsumer : IConsumer<IUpdateProductCommand>
    {
        private IProductService ProductService;

        public UpdateProductCommandConsumer(IProductService productService)
        {
            ProductService = productService;
        }

        public async Task Consume(ConsumeContext<IUpdateProductCommand> context)
        {
            var editedProduct = await ProductService.UpdateProduct(context.Message.Product);
            await Console.Out.WriteLineAsync($"Product {editedProduct.Id} was updated.");
            await context.Publish(MapProductUpdatedToMessaje(editedProduct, context.Message.Product));
        }

        private IProductUpdatedEvent MapProductUpdatedToMessaje(Product addedProduct, Messaging.Model.Product extraInformation)
        {
            return new ProductUpdatedEvent
            {
                Product = new Messaging.Model.Product
                {
                    ProductId = addedProduct.Id,
                    CategoryId = (int)addedProduct.CategoryId,
                    Description = addedProduct.Description,
                    Name = addedProduct.Name,
                    Price = extraInformation.Price,
                    Inventory = extraInformation.Inventory,
                    AditionalDetails = addedProduct.AditionalDetails.Select(x => new Messaging.Model.AditionalDetail
                    {
                        AditionalDetailId = x.Id,
                        Name = x.Name,
                        Text = x.Text
                    }).ToList()
                }
            };
        }
    }
}
