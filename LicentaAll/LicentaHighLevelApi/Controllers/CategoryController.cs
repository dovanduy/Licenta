using System.Threading.Tasks;
using System.Web.Http;
using LicentaHighLevelApi.Model.DTOs;
using LicentaHighLevelApi.Services.Interfaces;

namespace LicentaHighLevelApi.Controllers
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

        public async Task<IHttpActionResult> Post(CategoryDTO category)
        {
            await _categoryService.Add(CategoryDTO.MapToBusContract(category));
            return Ok();
        }

        public async Task<IHttpActionResult> Put(CategoryDTO category)
        {
            await _categoryService.Update(CategoryDTO.MapToBusContract(category));
            return Ok();
        }

        public async Task<IHttpActionResult> Delete(int categoryId)
        {
            await _categoryService.Delete(categoryId);
            return Ok();
        }
    }
}
