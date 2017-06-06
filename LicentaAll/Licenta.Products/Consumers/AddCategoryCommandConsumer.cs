using Licenta.Messaging.Messages.Commands;
using Licenta.Products.Messages;
using MassTransit;
using System;
using System.Threading.Tasks;
using Licenta.Products.Services.Interfaces;

namespace Licenta.Products.Consumers
{
    class AddCategoryCommandConsumer : IConsumer<IAddCategoryCommand>
    {
        private ICategoryService CategoryService;

        public AddCategoryCommandConsumer(ICategoryService categoryService)
        {
            CategoryService = categoryService;
        }

        public async Task Consume(ConsumeContext<IAddCategoryCommand> context)
        {
            var addedCategory = await CategoryService.AddNewCategory(context.Message.Category);
            await Console.Out.WriteLineAsync($"Category {addedCategory.Id} : '{addedCategory.Name}' was added.");
            await context.Publish(MapCategoryAddedToMessaje(addedCategory));
        }

        private CategoryUpdatedEvent MapCategoryAddedToMessaje(Category addedCategory)
        {
            return new CategoryUpdatedEvent
            {
                Category = new Messaging.Model.Category
                {
                    CategoryId = addedCategory.Id,
                    Name = addedCategory.Name,
                    Visible = addedCategory.Visible
                }
            };
        }
    }
}
