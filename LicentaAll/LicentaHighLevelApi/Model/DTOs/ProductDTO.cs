using System.Collections.Generic;
using System.Linq;

namespace LicentaHighLevelApi.Model.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int Inventory { get; set; }
        public int Version { get; set; }
        public IList<AditionalDetailDTO> AditionalDetails { get; set; }

        public static Licenta.Messaging.Model.Product MapToProductBusContract(ProductDTO productDto)
        {
            return new Licenta.Messaging.Model.Product
            {
                ProductId = productDto.ProductId,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                CategoryId = productDto.CategoryId,
                Inventory = productDto.Inventory,
                RowVersion = productDto.Version,
                AditionalDetails = productDto.AditionalDetails.Select(x => new Licenta.Messaging.Model.AditionalDetail
                {
                    AditionalDetailId = x.AditionalDetailId,
                    Name = x.Name,
                    Text = x.Text
                }).ToList()
            };
        }
    }
}