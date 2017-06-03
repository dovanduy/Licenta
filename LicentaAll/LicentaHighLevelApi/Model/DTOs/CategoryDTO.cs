using Licenta.Messaging.Model;

namespace LicentaHighLevelApi.Model.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public bool Visible { get; set; }

        public static Category MapToBusContract(CategoryDTO categoryDto)
        {
            return new Category
            {
                CategoryId = categoryDto.CategoryId,
                Name = categoryDto.Name,
                Visible = categoryDto.Visible
            };
        }
    }
}