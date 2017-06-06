using Licenta.Messaging.Messages.Commands;
using Licenta.Messaging.Messages.Events;
using Licenta.Products.Messages;
using MassTransit;
using System;
using System.Threading.Tasks;
using Licenta.Products.Services.Interfaces;

namespace Licenta.Products.Consumers
{
    public class DeleteProductCommandConsumer : IConsumer<IDeleteProductCommand>
    {
        private IProductService ProductService;

        public DeleteProductCommandConsumer(IProductService productService)
        {
            ProductService = productService;
        }

        public async Task Consume(ConsumeContext<IDeleteProductCommand> context)
        {
            await ProductService.DeleteProduct(context.Message.ProductId);
            await Console.Out.WriteLineAsync($"Product {context.Message.ProductId} was deleted.");
            await context.Publish(CreateProductDeletedEvent(context.Message.ProductId));
        }

        private IProductDeletedEvent CreateProductDeletedEvent(int productId)
        {
            return new ProductDeletedEvent { ProductId = productId };
        }
    }
}
