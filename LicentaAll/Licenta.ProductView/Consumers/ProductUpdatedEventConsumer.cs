using System;
using System.Threading.Tasks;
using Licenta.Messaging.Messages.Events;
using MassTransit;
using Licenta.ProductView.EntityFramework.Enumerations;
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
                var productToEdit = context.Message.Product;

                var aditionalDetails = productToEdit.AditionalDetails.Select(x => new AditionalDetail
                {
                    AditionalDetailId = x.AditionalDetailId,
                    Name = x.Name,
                    Text = x.Text
                }).ToList();

                var editedProduct = unitOfWork.Products.First(x => x.ProductId == productToEdit.ProductId);

                unitOfWork.Products.Attach(editedProduct);

                editedProduct.Name = productToEdit.Name;
                editedProduct.Description = productToEdit.Description;
                editedProduct.CategoryId = (CategoryEnum)productToEdit.CategoryId;
                editedProduct.AditionalDetails = aditionalDetails;

                await unitOfWork.SaveChangesAsync();
                await Console.Out.WriteLineAsync($"Product {editedProduct.ProductId} was updated.");
            }
        }
    }
}
