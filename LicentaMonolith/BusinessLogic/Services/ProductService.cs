using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ApiContracts.Dtos;
using BusinessLogic.Mappers;
using BusinessLogic.Services.Interfaces;
using Contracts.DataAccess;
using DataAccess;
using Microsoft.Practices.ObjectBuilder2;

namespace BusinessLogic.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Product> _repositoryProduct;
        private readonly IRepository<AditionalDetail> _repositoryAditionalDetails;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repositoryProduct = unitOfWork.GetRepository<Product>();
            _repositoryAditionalDetails = unitOfWork.GetRepository<AditionalDetail>();
        }

        public IList<ProductDto> GetForList(int categoryId)
        {
            return _repositoryProduct.AllEntities().Where(x => x.CategoryId == categoryId).Select(ProductMapper.Map).ToList();
        }

        public ProductDto GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void AddNewProduct(ProductDto product)
        {
            _repositoryProduct.Add(ProductMapper.MapWithAditionalDetails(product));
            _unitOfWork.SaveChanges();
        }

        public void DeleteProduct(int productId)
        {
            _repositoryProduct.Get(productId).AditionalDetails.ForEach(x => _repositoryAditionalDetails.Delete(x.Id));
            _repositoryProduct.Delete(productId);
            _unitOfWork.SaveChanges();
        }

        public void UpdateProduct(ProductDto product)
        {
            product.AditionalDetails
                .Select(x => AditionalDetailMapper.Map(x, product.ProductId.Value))
                .ForEach(x => _repositoryAditionalDetails.Update(x));

            _repositoryProduct.Update(ProductMapper.Map(product));

            _unitOfWork.SaveChanges();
        }
    }
}
