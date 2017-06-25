using System.Threading.Tasks;

namespace Licenta.ProductView.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<EntityFramework.Category> AddNewCategory(Messaging.Model.Category productToAdd);
        Task<int> DeleteCategory(int categoryId);
        Task<EntityFramework.Category> UpdateCategory(Messaging.Model.Category productToEdit);
    }
}
