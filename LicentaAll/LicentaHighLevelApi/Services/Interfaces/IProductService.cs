using Licenta.Messaging.Model;
using LicentaHighLevelApi.Model;
using LicentaHighLevelApi.Model.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LicentaHighLevelApi.Services.Interfaces
{
    public interface IProductService
    {
        Task<IList<ProductListDTO>> GetAllProductsForList();
        Task<ProductDTO> GetById(int Id);
        Task Add(Product product);
        Task Update(Product product);
        Task Delete(Product product);
        Task Delete(int productId);
    }
}
