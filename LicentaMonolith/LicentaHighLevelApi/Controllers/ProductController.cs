using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BusinessLogic.Mappers;
using BusinessLogic.Services.Interfaces;
using Contracts.ApiDtos;

namespace LicentaHighLevelApi.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IList<ProductDto> Get()
        {
            return _productService.GetWithoutAditionalDetails().Select(ProductMapper.MapWithAditionalDetails).ToList();
        }

        public ProductDto GetById(int id)
        {
            return  _productService
                .Get(x => x.Where(y => y.Id == id))
                .Select(ProductMapper.MapWithAditionalDetails)
                .First();
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
