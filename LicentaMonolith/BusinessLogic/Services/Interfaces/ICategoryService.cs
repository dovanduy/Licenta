using System;
using System.Collections.Generic;
using System.Linq;
using Contracts.ApiDtos;
using DataAccess;

namespace BusinessLogic.Services.Interfaces
{
    public interface ICategoryService
    {
        void AddNewCategory(CategoryDto category);
        void DeleteCategory(int categoryId);
        void UpdateCategory(CategoryDto category);
        IList<Category> Get(Func<IQueryable<Category>, IQueryable<Category>> query = null);
    }
}