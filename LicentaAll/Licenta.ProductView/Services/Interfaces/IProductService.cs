using System.Threading.Tasks;

namespace Licenta.ProductView.Services.Interfaces
{
    public interface IProductService
    {
        Task<EntityFramework.Product> AddNewProduct(Messaging.Model.Product productToAdd);
        Task<int> DeleteProduct(int categoryId);
        Task<EntityFramework.Product> UpdateProduct(Messaging.Model.Product productToEdit);
        Task<EntityFramework.Product> UpdateProduct(int productId, int inventory = -1, int rating = -1, decimal price = -1);
    }
}