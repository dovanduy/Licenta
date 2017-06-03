using System.Threading.Tasks;
using Licenta.Messaging.Model;

namespace LicentaHighLevelApi.Services.Interfaces
{
    public interface ICategoryService
    {
        Task Add(Category category);
        Task Delete(int categoryId);
        Task Delete(Category category);
        Task Update(Category category);
    }
}