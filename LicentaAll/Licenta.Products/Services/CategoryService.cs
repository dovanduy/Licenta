using System.Data.Entity.Core;
using System.Linq;
using System.Threading.Tasks;
using Licenta.EntityFramework.UnitOfWork.Interfaces;
using Licenta.Products.Services.Interfaces;

namespace Licenta.Products.Services
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

        public async Task<Category> AddNewCategory(Messaging.Model.Category categoryToAdd)
        {
            var addedCategory = new Category
            {
                Name = categoryToAdd.Name,
                Visible = categoryToAdd.Visible
            };
            _repository.Add(addedCategory);

            await _unitOfWork.SaveChangesAsync();

            return addedCategory;
        }

        public async Task DeleteCategory(int categoryId)
        {
            if (!_repository.AllEntities().Any(x => x.Id == categoryId))
                throw new EntityCommandExecutionException($"No category with id {categoryId}");

            _repositoryProduct.AllEntities()
                .Where(x => x.CategoryId == categoryId).ToList()
                .ForEach(x =>
                {
                    _repositoryProduct.Update(x);
                    x.CategoryId = 0;
                });
            _repository.Delete(categoryId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Category> UpdateCategory(Messaging.Model.Category categoryToEdit)
        {
            if(!_repository.AllEntities().Any(x => x.Id == categoryToEdit.CategoryId))
                throw new EntityCommandExecutionException($"No category with id {categoryToEdit.CategoryId}");

            var editedCategory = _repository.AllEntities().First(x => x.Id == categoryToEdit.CategoryId);

            _repository.Update(editedCategory);
                editedCategory.Name = categoryToEdit.Name;
                editedCategory.Visible = categoryToEdit.Visible;

            await _unitOfWork.SaveChangesAsync();

            return editedCategory;
        }
    }
}
