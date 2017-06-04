using System.Collections.Generic;

namespace Contracts.ApiDtos
{
    public class ProductDto
    {
        public int? ProductId { get; set; } 
        public string Name { get; set; } 
        public string Description { get; set; }
        public int CategoryId { get; set; } 
        public int? RowVersion { get; set; } 
        public decimal Price { get; set; } 
        public int Inventory { get; set; }
        public IEnumerable<AditionalDetailDto> AditionalDetails { get; set; }
    }
}
