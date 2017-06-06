using Licenta.Messaging.Messages.Commands;
using Licenta.Messaging.Messages.Events;
using Licenta.Products.Messages;
using MassTransit;
using System;
using System.Threading.Tasks;
using Licenta.Products.Services.Interfaces;

namespace Licenta.Products.Consumers
{
    class UpdateCategoryCommandConsumer : IConsumer<IUpdateCategoryCommand>
    {
        private ICategoryService CategoryService;

        public UpdateCategoryCommandConsumer(ICategoryService categoryService)
        {
            CategoryService = categoryService;
        }

        public async Task Consume(ConsumeContext<IUpdateCategoryCommand> context)
        {
            var editedCategory = await CategoryService.UpdateCategory(context.Message.Category);
            await Console.Out.WriteLineAsync($"Category {editedCategory.Id} was updated");
            await context.Publish(MapCategoryAddedToMessaje(editedCategory));
        }

        private ICategoryUpdatedEvent MapCategoryAddedToMessaje(Category addedCategory)
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
