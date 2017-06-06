using Licenta.Messaging.Messages.Commands;
using Licenta.Messaging.Messages.Events;
using Licenta.Products.Messages;
using MassTransit;
using System;
using System.Threading.Tasks;
using Licenta.Products.Services.Interfaces;

namespace Licenta.Products.Consumers
{
    public class DeleteCategoryCommandConsumer : IConsumer<IDeleteCategoryCommand>
    {
        private ICategoryService CategoryService;

        public DeleteCategoryCommandConsumer(ICategoryService categoryService)
        {
            CategoryService = categoryService;
        }

        public async Task Consume(ConsumeContext<IDeleteCategoryCommand> context)
        {
            await CategoryService.DeleteCategory(context.Message.CategoryId);
            await Console.Out.WriteLineAsync($"Category {context.Message.CategoryId} was deleted.");
            await context.Publish(CreateCategoryDeletedEvent(context.Message.CategoryId));
        }

        private ICategoryDeletedEvent CreateCategoryDeletedEvent(int categoryId)
        {
            return new CategoryDeletedEvent { CategoryId = categoryId };
        }
    }
}
