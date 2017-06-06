using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licenta.EntityFramework.UnitOfWork.Interfaces;
using Licenta.Products.Services.Interfaces;
using Microsoft.Practices.ObjectBuilder2;

namespace Licenta.Products.Services
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

        public async Task<Product> AddNewProduct(Messaging.Model.Product productToAdd)
        {
            var aditionalDetails = productToAdd.AditionalDetails.Select(x => new AditionalDetail
            {
                Id = x.AditionalDetailId,
                Name = x.Name,
                Text = x.Text
            }).ToList();

            var addedProduct = new Product
            {
                CategoryId = productToAdd.CategoryId,
                Description = productToAdd.Description,
                Name = productToAdd.Name,
                AditionalDetails = aditionalDetails
            };

            _repositoryProduct.Add(addedProduct);
            await _unitOfWork.SaveChangesAsync();
            return addedProduct;
        }

        public async Task DeleteProduct(int productId)
        {
            if (!_repositoryProduct.AllEntities().Any(x => x.Id == productId))
                throw new EntityCommandExecutionException($"No product with id {productId}");

            _repositoryProduct.Get(productId).AditionalDetails.ForEach(x => _repositoryAditionalDetails.Delete(x.Id));
            _repositoryProduct.Delete(productId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Product> UpdateProduct(Messaging.Model.Product productToEdit)
        {
            if (!_repositoryProduct.AllEntities().Any(x => x.Id == productToEdit.ProductId))
                throw new EntityCommandExecutionException($"No product with id {productToEdit.ProductId}");

            var aditionalDetailsToAdd = productToEdit.AditionalDetails
                .Where(x => x.AditionalDetailId == 0)
                .Select(MapToDatabaseEntity)
                .ToList();

            var aditionalDetailsToEdit = productToEdit.AditionalDetails
                .Where(x => x.AditionalDetailId != 0)
                .ToList();

            var editedProduct = _repositoryProduct.AllEntities().First(x => x.Id == productToEdit.ProductId);

            _repositoryProduct.Update(editedProduct);

            editedProduct.Name = productToEdit.Name;
            editedProduct.Description = productToEdit.Description;
            editedProduct.CategoryId = productToEdit.CategoryId;

            aditionalDetailsToEdit.ForEach(x =>
            {
                var editedDetail = editedProduct.AditionalDetails.First(y => y.Id == x.AditionalDetailId);
                _repositoryAditionalDetails.Update(editedDetail);
                editedDetail.Name = x.Name;
                editedDetail.Text = x.Text;
            });

            aditionalDetailsToAdd.ForEach(x =>
            {
                _repositoryAditionalDetails.Add(x);
                editedProduct.AditionalDetails.Add(x);
            });

            await _unitOfWork.SaveChangesAsync();
            return editedProduct;
        }

        private AditionalDetail MapToDatabaseEntity(Messaging.Model.AditionalDetail aditionalDetail)
        {
            return new AditionalDetail
            {
                Id = aditionalDetail.AditionalDetailId,
                Name = aditionalDetail.Name,
                Text = aditionalDetail.Text
            };
        }
    }
}
