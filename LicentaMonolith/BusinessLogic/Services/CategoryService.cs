using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Mappers;
using BusinessLogic.Services.Interfaces;
using Contracts.ApiDtos;
using Contracts.DataAccess;
using DataAccess;

namespace BusinessLogic.Services
{
    public class CategoryService : ICategoryService
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<Category> _repository;
        private IRepository<Product> _repositoryProduct;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<Category>();
            _repositoryProduct = _unitOfWork.GetRepository<Product>();
        }

        public void AddNewCategory(CategoryDto category)
        {
            _repository.Add(CategoryMapper.Map(category));
            _unitOfWork.SaveChanges();
        }

        public void DeleteCategory(int categoryId)
        {
            _repositoryProduct.All()
                .Where(x => x.CategoryId == categoryId).ToList()
                .ForEach(x => { x.CategoryId = 0; }); 
            _repository.Delete(categoryId);
            _unitOfWork.SaveChanges();
        }

        public void UpdateCategory(CategoryDto category)
        {
            _repository.Update(CategoryMapper.Map(category));
            _unitOfWork.SaveChanges();
        }

        public IList<Category> Get(Func<IQueryable<Category>, IQueryable<Category>> query = null)
        {
            if (query == null)
            {
                return _repository.All().ToList();
            }
            return query.Invoke(_repository.All()).ToList();
        }
    }
}
