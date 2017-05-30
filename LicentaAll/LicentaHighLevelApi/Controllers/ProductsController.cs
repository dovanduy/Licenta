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
    [RoutePrefix("api/Products")]
    public class ProductsController : ApiController
    {
        private IProductsService _ProductsService;

        public ProductsController(IProductsService productsService)
        {
            _ProductsService = productsService;
        }

        public async Task<IList<ProductListDTO>> Get()
        {
            return await _ProductsService.GetAllProductsForList();
        }

        public async Task<ProductDTO> GetById(int Id)
        {
            return await _ProductsService.GetById(Id);
        }

        public async Task<IHttpActionResult> Post(ProductDTO product)
        {
            await _ProductsService.AddProduct(ProductDTO.MapToProductMessage(product));
            return Ok();
        }

        public async Task<IHttpActionResult> Put(ProductDTO product)
        {
            await _ProductsService.UpdateProduct(ProductDTO.MapToProductMessage(product));
            return Ok();
        }
    }
}
