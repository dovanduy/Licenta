using Licenta.Messaging.Messages;
using Licenta.Products.Messages;
using MassTransit;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Products.Consumers
{
    public class AddProductCommandConsumer : IConsumer<IAddProductCommand>
    {
        public async Task Consume(ConsumeContext<IAddProductCommand> context)
        {
            using (ProductsDbContext unitOfWork = new ProductsDbContext())
            {
                var productToAdd = context.Message.Product;

                var aditionalDetails = productToAdd.AditionalDetails.Select(x => new AditionalDetail
                {
                    AditionalDetailId = x.AditionalDetailId,
                    Name = x.Name,
                    Text = x.Text
                }).ToList();

                var addedProduct = new Product
                {
                    CategoryId = (Enumerations.CategoryEnum)productToAdd.CategoryId,
                    Description = productToAdd.Description,
                    Name = productToAdd.Name,
                    AditionalDetails = aditionalDetails
                };

                unitOfWork.Products.Add(addedProduct);

                await unitOfWork.SaveChangesAsync();
                await Console.Out.WriteLineAsync($"Product {addedProduct.ProductId} was added.");

                await context.Publish(MapProductAddedToMessaje(addedProduct));

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                await Console.Out.WriteLineAsync("Event published: ProductAddedEvent");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private IProductAddedEvent MapProductAddedToMessaje(Product addedProduct)
        {
            return new ProductAddedEvent
            {
                Product = new Messaging.Model.Product
                {
                    ProductId = addedProduct.ProductId,
                    CategoryId = (int)addedProduct.CategoryId,
                    Description = addedProduct.Description,
                    Name = addedProduct.Name,
                    AditionalDetails = addedProduct.AditionalDetails.Select(x => new Messaging.Model.AditionalDetail
                    {
                        AditionalDetailId = x.AditionalDetailId,
                        Name = x.Name,
                        Text = x.Text
                    }).ToList()
                }
            };
        }
    }
}
