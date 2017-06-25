using System.Collections.Generic;
using ApiContracts.Dtos;

namespace BusinessLogic.Services.Interfaces
{
    public interface ICategoryService
    {
        void AddNewCategory(CategoryDto category);
        void DeleteCategory(int categoryId);
        void UpdateCategory(CategoryDto category);
        IList<CategoryDto> GetCategories();
    }
}