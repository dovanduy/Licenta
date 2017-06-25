using System.Collections.Generic;
using ApiContracts.Dtos;

namespace BusinessLogic.Services.Interfaces
{
    public interface IProductService
    {
        IList<ProductDto> GetForList(int categoryId);
        ProductDto GetById(int id);
        void AddNewProduct(ProductDto product);
        void DeleteProduct(int productId);
        void UpdateProduct(ProductDto product);
    }
}