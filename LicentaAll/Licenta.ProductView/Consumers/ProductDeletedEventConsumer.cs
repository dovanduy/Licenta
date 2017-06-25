using System;
using System.Threading.Tasks;
using Licenta.Messaging.Messages.Events;
using MassTransit;
using Licenta.ProductView.Services.Interfaces;

namespace Licenta.ProductView.Consumers 
{
    class ProductDeletedEventConsumer : IConsumer<IProductDeletedEvent>
    {
        private IProductService _productService;

        public ProductDeletedEventConsumer(IProductService productService)
        {
            _productService = productService;
        }

        public async Task Consume(ConsumeContext<IProductDeletedEvent> context)
        {
            await _productService.DeleteProduct(context.Message.ProductId);
            await Console.Out.WriteLineAsync($"Category {context.Message.ProductId} was deleted.");
        }
    }
}
