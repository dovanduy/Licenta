using Licenta.Messaging.Messages.Events;
using Licenta.ProductView.EntityFramework;
using MassTransit;
using System;
using System.Data.Entity.Core;
using System.Linq;
using System.Threading.Tasks;
using Licenta.ProductView.Services.Interfaces;

namespace Licenta.ProductView.Consumers
{
    public class ProductPriceUpdatedEventConsumer : IConsumer<IProductPriceUpdatedEvent>
    {
        private IProductService _productService;

        public ProductPriceUpdatedEventConsumer(IProductService productService)
        {
            _productService = productService;
        }

        public async Task Consume(ConsumeContext<IProductPriceUpdatedEvent> context)
        {
            var productId = context.Message.ProductId;
            var updatedPrice = context.Message.Price;

            var updatedProduct = _productService.UpdateProduct(productId, price: updatedPrice);
            await Console.Out.WriteLineAsync($"Product {updatedProduct.Id} inventory was changed.");
        }
    }
}
