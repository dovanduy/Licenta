using System.Linq;
using BusinessLogic.Services.Interfaces;
using Contracts.DataAccess;
using DataAccess;

namespace BusinessLogic.Services
{
    class InventoryService : IInventoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Inventory> _repositoryInventory;

        public InventoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repositoryInventory = unitOfWork.GetRepository<Inventory>();
        }

        public int GetProductInventory(int productId)
        {
            if (_repositoryInventory.AllEntities().Any(x => x.ProductId == productId))
            {
                return _repositoryInventory.AllEntities().Count(x => x.ProductId == productId && !x.DateDeleted.HasValue);
            }
            return 0;
        }
    }
}
