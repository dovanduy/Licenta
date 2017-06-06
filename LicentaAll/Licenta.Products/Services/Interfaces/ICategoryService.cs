using System.Threading.Tasks;

namespace Licenta.Products.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> AddNewCategory(Messaging.Model.Category categoryToAdd);
        Task DeleteCategory(int categoryId);
        Task<Category> UpdateCategory(Messaging.Model.Category categoryToEdit);
    }
}