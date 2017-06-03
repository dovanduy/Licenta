using Licenta.Messaging;
using Licenta.Messaging.Messages;
using Licenta.Messaging.Model;
using LicentaHighLevelApi.Model.DTOs;
using LicentaHighLevelApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

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

        public async Task<IList<ProductListDTO>> Get()
        {
            return await _productService.GetAllProductsForList();
        }

        public async Task<ProductDTO> GetById(int Id)
        {
            return await _productService.GetById(Id);
        }

        public async Task<IHttpActionResult> Post(ProductDTO product)
        {
            await _productService.Add(ProductDTO.MapToProductBusContract(product));
            return Ok();
        }

        public async Task<IHttpActionResult> Put(ProductDTO product)
        {
            await _productService.Update(ProductDTO.MapToProductBusContract(product));
            return Ok();
        }

        public async Task<IHttpActionResult> Delete(int productId)
        {
            await _productService.Delete(productId);
            return Ok();
        }
    }
}
