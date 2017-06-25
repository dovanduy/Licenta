using System.Collections.Generic;
using System.Web.Http;
using ApiContracts.Dtos;
using BusinessLogic.Services.Interfaces;

namespace LicentaMonolithHighLevelApi.Controllers
{
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [AllowAnonymous]
        [Route("Category/{categoryId}")]
        public IList<ProductDto> Get(int categoryId)
        {
            return _productService.GetForList(categoryId);
        }

        [AllowAnonymous]
        public ProductDto GetById(int id)
        {
            return _productService.GetById(id);
        }

        public IHttpActionResult Post(ProductDto product)
        {
            _productService.AddNewProduct(product);
            return Ok();
        }

        public IHttpActionResult Put(ProductDto product)
        {
             _productService.UpdateProduct(product);
            return Ok();
        }

        public IHttpActionResult Delete(int productId)
        {
             _productService.DeleteProduct(productId);
            return Ok();
        }
    }
}
