using System;
using System.Threading.Tasks;
using Licenta.Messaging.Messages.Events;
using MassTransit;
using Licenta.ProductView.EntityFramework;
using System.Linq;
using System.Data.Entity.Core;
using System.Collections.Generic;
using Licenta.ProductView.Services.Interfaces;

namespace Licenta.ProductView.Consumers
{
    class CategoryDeletedEventConsumer : IConsumer<ICategoryDeletedEvent>
    {
        private ICategoryService _categoryService;

        public CategoryDeletedEventConsumer(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task Consume(ConsumeContext<ICategoryDeletedEvent> context)
        {
            var idOfCategoryToBeDeleted = context.Message.CategoryId;
            await _categoryService.DeleteCategory(idOfCategoryToBeDeleted);
            await Console.Out.WriteLineAsync($"Category {idOfCategoryToBeDeleted} was deleted.");
        }
    }
}
