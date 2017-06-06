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
    public class AddProductCommandConsumer : IConsumer<IAddProductCommand>
    {
        private IProductService ProductService;

        public AddProductCommandConsumer(IProductService productService)
        {
            ProductService = productService;
        }

        public async Task Consume(ConsumeContext<IAddProductCommand> context)
        {
            var addedProduct = await ProductService.AddNewProduct(context.Message.Product);
            await Console.Out.WriteLineAsync($"Product {addedProduct.Id} was added.");
            await context.Publish(MapProductAddedToMessaje(addedProduct, context.Message.Product));
        }

        private IProductUpdatedEvent MapProductAddedToMessaje(Product addedProduct, Messaging.Model.Product extraInformation)
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
