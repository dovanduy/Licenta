using System;
using System.Collections.Generic;
using System.Linq;
using Contracts.ApiDtos;
using DataAccess;

namespace BusinessLogic.Services.Interfaces
{
    public interface IProductService
    {
        void AddNewProduct(ProductDto product);
        void DeleteProduct(int productId);
        IList<Product> Get(Func<IQueryable<Product>, IQueryable<Product>> query = null);
        IList<Product> GetWithoutAditionalDetails(Func<IQueryable<Product>, IQueryable<Product>> query = null);
        void UpdateProduct(ProductDto product);
    }
}