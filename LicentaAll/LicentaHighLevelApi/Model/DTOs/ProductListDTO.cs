namespace LicentaHighLevelApi.Model.DTOs
{
    public class ProductListDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsInStock { get; set; }
        public string CategoryName { get; set; }
    }
}