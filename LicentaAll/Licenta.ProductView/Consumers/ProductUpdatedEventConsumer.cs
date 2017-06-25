using System;
using System.Threading.Tasks;
using Licenta.Messaging.Messages.Events;
using MassTransit;
using Licenta.ProductView.EntityFramework;
using System.Linq;
using Licenta.ProductView.Services.Interfaces;

namespace Licenta.ProductView.Consumers
{
    public class ProductUpdatedEventConsumer : IConsumer<IProductUpdatedEvent>
    {
        private IProductService _productService;

        public ProductUpdatedEventConsumer(IProductService productService)
        {
            _productService = productService;
        }

        public async Task Consume(ConsumeContext<IProductUpdatedEvent> context)
        {
            var productInQuestion = context.Message.Product;
            var updatedProduct = await _productService.UpdateProduct(productInQuestion);
            if(updatedProduct.RowVersion > 1)
                await Console.Out.WriteLineAsync($"Product {updatedProduct.Id} was Updated.");
            else
                await Console.Out.WriteLineAsync($"Product {updatedProduct.Id} was Added.");
        }
    }
}
