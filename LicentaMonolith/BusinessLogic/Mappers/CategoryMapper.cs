using Contracts.ApiDtos;
using DataAccess;

namespace BusinessLogic.Mappers
{
    public static class CategoryMapper
    {
        public static Category Map(CategoryDto dto)
        {
            return new Category
            {
                Id = dto.CategoryId ?? 0,
                RowVersion = dto.RowVersion ?? 1,
                Name = dto.Name,
                Visible = dto.Visible
            };
        }

        public static CategoryDto Map(Category category)
        {
            return new CategoryDto
            {
                CategoryId = category.Id,
                RowVersion = category.RowVersion,
                Name = category.Name,
                Visible = category.Visible
            };
        }
    }
}
