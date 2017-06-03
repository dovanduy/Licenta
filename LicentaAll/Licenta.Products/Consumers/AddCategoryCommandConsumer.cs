using Licenta.Messaging.Messages.Commands;
using Licenta.Messaging.Messages.Events;
using Licenta.Products.Messages;
using MassTransit;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Products.Consumers
{
    class AddCategoryCommandConsumer : IConsumer<IAddCategoryCommand>
    {
        public async Task Consume(ConsumeContext<IAddCategoryCommand> context)
        {
            using (ProductsDbContext unitOfWork = new ProductsDbContext())
            {
                var categoryToAdd = context.Message.Category;
                await Console.Out.WriteLineAsync("Category add command recieved.");
                var addedCategory = new Category
                {
                    Name = categoryToAdd.Name,
                    Visible = categoryToAdd.Visible
                };

                unitOfWork.Categories.Add(addedCategory);

                await unitOfWork.SaveChangesAsync();
                await Console.Out.WriteLineAsync($"Category {addedCategory.CategoryId} : '{addedCategory.Name}' was added.");

                await context.Publish(MapCategoryAddedToMessaje(addedCategory));

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                await Console.Out.WriteLineAsync("Event published: CategoryUpdatedEvent");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        private CategoryUpdatedEvent MapCategoryAddedToMessaje(Category addedCategory)
        {
            return new CategoryUpdatedEvent
            {
                Category = new Messaging.Model.Category
                {
                    CategoryId = addedCategory.CategoryId,
                    Name = addedCategory.Name,
                    Visible = addedCategory.Visible
                }
            };
        }
    }
}
