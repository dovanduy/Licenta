using System;
using System.Threading.Tasks;
using Licenta.Messaging.Messages.Events;
using MassTransit;
using Licenta.ProductView.EntityFramework;
using System.Linq;

namespace Licenta.ProductView.Consumers
{
    class CategoryUpdatedEventConsumer : IConsumer<ICategoryUpdatedEvent>
    {
        public async Task Consume(ConsumeContext<ICategoryUpdatedEvent> context)
        {
            using (ProductViewDbContext unitOfWork = new ProductViewDbContext())
            {
                var categoryInQuestion = context.Message.Category;

                if (unitOfWork.Categories.Any(x => x.CategoryId == categoryInQuestion.CategoryId))
                {
                    var editedCategory = unitOfWork.Categories.First(x => x.CategoryId == categoryInQuestion.CategoryId);
                    string message = "";

                    unitOfWork.Categories.Attach(editedCategory);
                    if (editedCategory.Name != categoryInQuestion.Name)
                    {
                        message += $"Name: '{editedCategory.Name}' > '{categoryInQuestion.Name}' ";
                        editedCategory.Name = categoryInQuestion.Name;
                    }
                    if (editedCategory.Visible != categoryInQuestion.Visible)
                    {
                        message += $"Visible: '{editedCategory.Visible}' > '{categoryInQuestion.Visible}' ";
                        editedCategory.Visible = categoryInQuestion.Visible;
                    }

                    await unitOfWork.SaveChangesAsync();
                    await Console.Out.WriteLineAsync($"Category {editedCategory.CategoryId} was updated: {message}");
                }
                else
                {
                    unitOfWork.Categories.Add(new Category
                    {
                        CategoryId = categoryInQuestion.CategoryId,
                        Name = categoryInQuestion.Name,
                        Visible = categoryInQuestion.Visible
                    });

                    await unitOfWork.SaveChangesAsync();
                    await Console.Out.WriteLineAsync($"Category {categoryInQuestion.CategoryId} : '{categoryInQuestion.Name}' was added.");
                }
            }
        }
    }
}
