using System.Web.Http;
using ApiContracts.Dtos;
using BusinessLogic.Services.Interfaces;

namespace LicentaMonolithHighLevelApi.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/Category")]
    public class CategoryController : ApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IHttpActionResult Post(CategoryDto category)
        {
            _categoryService.AddNewCategory(category);
            return Ok();
        }

        public IHttpActionResult Put(CategoryDto category)
        {
            _categoryService.UpdateCategory(category);
            return Ok();
        }

        public IHttpActionResult Delete(int categoryId)
        {
            _categoryService.DeleteCategory(categoryId);
            return Ok();
        }
    }
}
