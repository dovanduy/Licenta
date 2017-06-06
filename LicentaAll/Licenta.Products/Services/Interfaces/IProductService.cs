using System.Threading.Tasks;

namespace Licenta.Products.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> AddNewProduct(Messaging.Model.Product productToAdd);
        Task DeleteProduct(int productId);
        Task<Product> UpdateProduct(Messaging.Model.Product productToEdit);
    }
}