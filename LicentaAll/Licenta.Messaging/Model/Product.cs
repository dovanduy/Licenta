using System.Collections.Generic;

namespace Licenta.Messaging.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int Inventory { get; set; }
        public int RowVersion { get; set; }
        public IList<AditionalDetail> AditionalDetails { get; set; }
    }
}
