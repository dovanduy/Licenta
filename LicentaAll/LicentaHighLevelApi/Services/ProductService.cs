using Licenta.Messaging.Messages.Commands;
using Licenta.Messaging.Model;
using LicentaHighLevelApi.Model.DTOs;
using LicentaHighLevelApi.Model.Messages;
using LicentaHighLevelApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Licenta.ViewContracts;
using System.Linq;
using System.IO;

namespace LicentaHighLevelApi.Services
{
    public class ProductService : IProductService
    {
        public async Task<IList<ProductListDTO>> GetAllProductsForList()
        {
            using(HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.GetAsync("http://localhost/LicentaProductView/api/Products");
                IList<ProductViewDto> products;

                using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                using (var jsonReader = new JsonTextReader(reader))
                {
                    products = JsonSerializer.Create().Deserialize<IList<ProductViewDto>>(jsonReader);
                }

                return products.Select(x => new ProductListDTO {
                    ProductId = x.ProductId,
                    Name = x.Name,
                    Description = x.Description,
                    IsInStock = x.IsInStock.HasValue ? x.IsInStock.Value : false,
                    CategoryName = x.CategoryName,
                    Price = x.Price.HasValue ? x.Price.Value : 0
                }).ToList();
            }
        }

        public async Task<ProductDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Add(Product product)
        {
            using (BusMessagingService busService = new BusMessagingService())
            {
                await busService.BusControl.Publish<IAddProductCommand>(new AddProductCommand {Product = product});
            }
        }

        public async Task Update(Product product)
        {
            using (BusMessagingService busService = new BusMessagingService())
            {
                await busService.BusControl.Publish<IUpdateProductCommand>(new UpdateProductCommand {Product = product});
            }
        }

        public async Task Delete(Product product)
        {
            await Delete(product.ProductId);
        }

        public async Task Delete(int productId)
        {
            using (BusMessagingService busService = new BusMessagingService())
            {
                await busService.BusControl.Publish<IDeleteProductCommand>(new DeleteProductCommand {ProductId = productId});
            }
        }
    }
}