using System.Linq;
using Contracts.ApiDtos;
using DataAccess;

namespace BusinessLogic.Mappers
{
    public static class ProductMapper
    {
        public static Product Map(ProductDto dto)
        {
            return new Product
            {
                Id = dto.ProductId ?? 0,
                RowVersion = dto.RowVersion ?? 1,
                CategoryId = dto.CategoryId,
                Description = dto.Description,
                Inventory = dto.Inventory,
                Name = dto.Name,
                Price = dto.Price
            };
        }

        public static ProductDto Map(Product dto)
        {
            return new ProductDto
            {
                ProductId = dto.Id,
                RowVersion = dto.RowVersion,
                CategoryId = dto.CategoryId,
                Description = dto.Description,
                Inventory = dto.Inventory,
                Name = dto.Name,
                Price = dto.Price
            };
        }

        public static Product MapWithAditionalDetails(ProductDto product)
        {
            var dto = Map(product);
            dto.AditionalDetails = product.AditionalDetails
                .Select(x => AditionalDetailMapper.Map(x,dto.Id))
                .ToList();

            return dto;
        }

        public static ProductDto MapWithAditionalDetails(Product product)
        {
            var dto = Map(product);
            dto.AditionalDetails = product.AditionalDetails
                .Select(AditionalDetailMapper.Map)
                .ToList();

            return dto;
        }
    }
}
