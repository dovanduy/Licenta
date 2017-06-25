using System;
using System.Data.Entity.Core;
using System.Linq;
using System.Threading.Tasks;
using Licenta.Messaging.Messages.Events;
using Licenta.ProductView.EntityFramework;
using Licenta.ProductView.Services.Interfaces;
using MassTransit;

namespace Licenta.ProductView.Consumers
{
    public class ProductRatingUpdatedEventConsumer : IConsumer<IProductRatingUpdatedEvent> 
    {
        private IProductService _productService;

        public ProductRatingUpdatedEventConsumer(IProductService productService)
        {
            _productService = productService;
        }

        public async Task Consume(ConsumeContext<IProductRatingUpdatedEvent> context)
        {
            var productId = context.Message.ProductId;
            var updatedRating = context.Message.Rating;

            var updatedProduct = _productService.UpdateProduct(productId, price: Convert.ToDecimal(updatedRating));
            await Console.Out.WriteLineAsync($"Product {updatedProduct.Id} inventory was changed.");
        }
    }
}
