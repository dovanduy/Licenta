using Licenta.Messaging.Messages.Commands;
using Licenta.Messaging.Messages.Events;
using Licenta.Products.Messages;
using MassTransit;
using System;
using System.Data.Entity.Core;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Products.Consumers
{
    class UpdateCategoryCommandConsumer : IConsumer<IUpdateCategoryCommand>
    {
        public async Task Consume(ConsumeContext<IUpdateCategoryCommand> context)
        {
            using (ProductsDbContext unitOfWork = new ProductsDbContext())
            {
                var categoryToEdit = context.Message.Category;
                await Console.Out.WriteLineAsync($"Category update command recieved for category {categoryToEdit.CategoryId}.");
                if (unitOfWork.Categories.Any(x => x.CategoryId == categoryToEdit.CategoryId))
                {
                    var editedCategory = unitOfWork.Categories.First(x => x.CategoryId == categoryToEdit.CategoryId);
                    string message = "";

                    unitOfWork.Categories.Attach(editedCategory);
                    if (editedCategory.Name != categoryToEdit.Name) {
                        message += $"Name: '{editedCategory.Name}' > '{categoryToEdit.Name}' ";
                        editedCategory.Name = categoryToEdit.Name;
                    }
                    if (editedCategory.Visible != categoryToEdit.Visible) {
                        message += $"Visible: '{editedCategory.Visible}' > '{categoryToEdit.Visible}' ";
                        editedCategory.Visible = categoryToEdit.Visible;
                    }

                    await unitOfWork.SaveChangesAsync();
                    await Console.Out.WriteLineAsync($"Category {editedCategory.CategoryId} was updated: {message}");

                    await context.Publish(MapCategoryAddedToMessaje(editedCategory));

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    await Console.Out.WriteLineAsync("Event published: CategoryUpdatedEvent");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    string message = $"No category with id {categoryToEdit.CategoryId}";
                    Console.ForegroundColor = ConsoleColor.Red;
                    await Console.Out.WriteLineAsync(message);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    throw new EntityCommandExecutionException(message);
                }
            }
        }

        private ICategoryUpdatedEvent MapCategoryAddedToMessaje(Category addedCategory)
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
