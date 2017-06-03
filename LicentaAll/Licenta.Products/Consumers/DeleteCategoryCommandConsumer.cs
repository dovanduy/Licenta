using Licenta.Messaging.Messages.Commands;
using Licenta.Messaging.Messages.Events;
using Licenta.Products.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Products.Consumers
{
    public class DeleteCategoryCommandConsumer : IConsumer<IDeleteCategoryCommand>
    {
        public async Task Consume(ConsumeContext<IDeleteCategoryCommand> context)
        {
            using (ProductsDbContext unitOfWork = new ProductsDbContext())
            {
                var idOfCategoryToBeDeleted = context.Message.CategoryId;

                await Console.Out.WriteLineAsync($"Category delete command recieved for product {idOfCategoryToBeDeleted}.");

                if (unitOfWork.Categories.Any(x => x.CategoryId == idOfCategoryToBeDeleted))
                {
                    Category editedCategory = unitOfWork.Categories.First(x => x.CategoryId == idOfCategoryToBeDeleted);
                    unitOfWork.Categories.Attach(editedCategory);
                    editedCategory.DateDeleted = DateTime.Now;
                    
                    await unitOfWork.SaveChangesAsync();
                    await Console.Out.WriteLineAsync($"Category {editedCategory.CategoryId} : {editedCategory.Name} was deleted.");

                    await context.Publish(CreateCategoryDeletedEvent(idOfCategoryToBeDeleted));

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    await Console.Out.WriteLineAsync("Event published: CategoryDeletedEvent");
                    Console.ForegroundColor = ConsoleColor.Gray;
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

        private void MoveProductsToUncategorized(IProductsDbContext unitOfWork, int idOfCategoryToBeDeleted)
        {
            IList<Product> productsToMove = unitOfWork.Products.Where(x => x.CategoryId == idOfCategoryToBeDeleted).ToList();

            foreach(Product p in productsToMove){
                unitOfWork.Products.Attach(p);
                p.CategoryId = 0;
            }
        }

        private ICategoryDeletedEvent CreateCategoryDeletedEvent(int categoryId)
        {
            return new CategoryDeletedEvent { CategoryId = categoryId };
        }
    }
}
