using Licenta.Messaging.Model;
using LicentaHighLevelApi.Model;
using LicentaHighLevelApi.Model.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LicentaHighLevelApi.Services.Interfaces
{
    public interface IProductsService
    {
        Task<IList<ProductListDTO>> GetAllProductsForList();
        Task<ProductDTO> GetById(int Id);
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
    }
}
