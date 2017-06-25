using Licenta.Messaging.Messages.Events;
using MassTransit;
using System;
using System.Threading.Tasks;
using Licenta.ProductView.Services.Interfaces;

namespace Licenta.ProductView.Consumers
{
    public class ProductInventoryUpdatedEventConsumer : IConsumer<IProductInventoryUpdatedEvent>
    {
        private IProductService _productService;

        public ProductInventoryUpdatedEventConsumer(IProductService productService)
        {
            _productService = productService;
        }

        public async Task Consume(ConsumeContext<IProductInventoryUpdatedEvent> context)
        {
            var updatedProductId = context.Message.ProductId;
            var updatedProductInventory = context.Message.Inventory;

            var updatedProduct = _productService.UpdateProduct(updatedProductId, inventory: updatedProductInventory);
            await Console.Out.WriteLineAsync($"Product {updatedProduct.Id} inventory was changed.");
        }
    }
}
