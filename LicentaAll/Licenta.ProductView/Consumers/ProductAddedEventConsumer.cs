using Licenta.Messaging.Messages;
using Licenta.ProductView.EntityFramework;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.ProductView.Consumers
{
    public class ProductAddedEventConsumer : IConsumer<IProductAddedEvent>
    {
        public async Task Consume(ConsumeContext<IProductAddedEvent> context)
        {
            using(ProductViewDbContext unitOfWork = new ProductViewDbContext())
            {
                var addedProduct = context.Message.Product;

                var aditionalDetails = addedProduct.AditionalDetails.Select(x => new AditionalDetail
                {
                    AditionalDetailId = x.AditionalDetailId,
                    Name = x.Name,
                    Text = x.Text,
                    ProductId = addedProduct.ProductId
                }).ToList();

                unitOfWork.Products.Add(new Product
                {
                    ProductId = addedProduct.ProductId,
                    CategoryId = (EntityFramework.Enumerations.CategoryEnum) addedProduct.CategoryId,
                    Description = addedProduct.Description,
                    Name = addedProduct.Name,
                    AditionalDetails = aditionalDetails
                });

                await unitOfWork.SaveChangesAsync();
                await Console.Out.WriteLineAsync($"Product {addedProduct.ProductId} was added.");
            }
        }
    }
}
