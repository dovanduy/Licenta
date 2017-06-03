using System;
using System.Threading.Tasks;
using Licenta.Messaging.Messages.Events;
using MassTransit;
using Licenta.ProductView.EntityFramework;
using System.Linq;
using System.Data.Entity.Core;
using System.Collections.Generic;

namespace Licenta.ProductView.Consumers
{
    class CategoryDeletedEventConsumer : IConsumer<ICategoryDeletedEvent>
    {
        public async Task Consume(ConsumeContext<ICategoryDeletedEvent> context)
        {
            using (ProductViewDbContext unitOfWork = new ProductViewDbContext())
            {
                var idOfCategoryToBeDeleted = context.Message.CategoryId;

                await Console.Out.WriteLineAsync($"Category deleted event recieved for product {idOfCategoryToBeDeleted}.");

                if (unitOfWork.Categories.Any(x => x.CategoryId == idOfCategoryToBeDeleted))
                {
                    Category editedCategory = unitOfWork.Categories.First(x => x.CategoryId == idOfCategoryToBeDeleted);
                    string message = $"Category {editedCategory.CategoryId} : {editedCategory.Name} was deleted.";

                    MoveProductsToUncategorized(unitOfWork, idOfCategoryToBeDeleted);

                    unitOfWork.Categories.Remove(editedCategory);

                    await unitOfWork.SaveChangesAsync();
                    await Console.Out.WriteLineAsync(message);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    await Console.Out.WriteLineAsync($"No category with id {idOfCategoryToBeDeleted}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    throw new EntityCommandExecutionException($"No category with id {idOfCategoryToBeDeleted}");
                }
            }
        }

        private void MoveProductsToUncategorized(IProductViewDbContext unitOfWork, int idOfCategoryToBeDeleted)
        {
            IList<Product> productsToMove = unitOfWork.Products.Where(x => x.CategoryId == idOfCategoryToBeDeleted).ToList();

            foreach (Product p in productsToMove)
            {
                unitOfWork.Products.Attach(p);
                p.CategoryId = 0;
            }
        }
    }
}
