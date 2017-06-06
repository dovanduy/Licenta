using System;
using System.Threading.Tasks;
using Licenta.Messaging.Messages.Events;
using MassTransit;
using Licenta.ProductView.EntityFramework;
using System.Linq;

namespace Licenta.ProductView.Consumers
{
    public class ProductUpdatedEventConsumer : IConsumer<IProductUpdatedEvent>
    {
        public async Task Consume(ConsumeContext<IProductUpdatedEvent> context)
        {
            using (ProductViewDbContext unitOfWork = new ProductViewDbContext())
            {
                var productInQuestion = context.Message.Product;

                var aditionalDetails = productInQuestion.AditionalDetails.Select(x => new AditionalDetail
                {
                    AditionalDetailId = x.AditionalDetailId,
                    ProductId = productInQuestion.ProductId,
                    Name = x.Name,
                    Text = x.Text
                }).ToList();

                if (unitOfWork.Products.Any(x => x.ProductId == productInQuestion.ProductId))
                {
                    var editedProduct = unitOfWork.Products.First(x => x.ProductId == productInQuestion.ProductId);

                    unitOfWork.Products.Attach(editedProduct);

                    editedProduct.Name = productInQuestion.Name;
                    editedProduct.Description = productInQuestion.Description;
                    editedProduct.CategoryId = productInQuestion.CategoryId;
                    editedProduct.AditionalDetails = aditionalDetails;

                    await unitOfWork.SaveChangesAsync();
                    await Console.Out.WriteLineAsync($"Product {productInQuestion.ProductId} was updated.");
                }
                else
                {
                    unitOfWork.Products.Add(new Product
                    {
                        ProductId = productInQuestion.ProductId,
                        CategoryId = productInQuestion.CategoryId,
                        Description = productInQuestion.Description,
                        Name = productInQuestion.Name,
                        AditionalDetails = aditionalDetails
                    });

                    await unitOfWork.SaveChangesAsync();
                    await Console.Out.WriteLineAsync($"Product {productInQuestion.ProductId} was added.");
                }
            }
        }
    }
}
