using System;
using System.Threading.Tasks;
using Licenta.Messaging.Messages.Events;
using MassTransit;
using Licenta.ProductView.EntityFramework;
using System.Linq;
using Licenta.ProductView.Services.Interfaces;

namespace Licenta.ProductView.Consumers
{
    class CategoryUpdatedEventConsumer : IConsumer<ICategoryUpdatedEvent>
    {
        private ICategoryService _categoryService;

        public CategoryUpdatedEventConsumer(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task Consume(ConsumeContext<ICategoryUpdatedEvent> context)
        {
            var categoryInQuestion = await _categoryService.UpdateCategory(context.Message.Category);
            if (categoryInQuestion.RowVersion > 1)
                await Console.Out.WriteLineAsync($"Category {categoryInQuestion.Id} was updated");
            else
                await Console.Out.WriteLineAsync($"Category {categoryInQuestion.Id} was added");
        }
    }
}
