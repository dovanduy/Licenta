using System.Threading.Tasks;
using Licenta.EntityFramework.UnitOfWork.Interfaces;

namespace Licenta.EntityFramework.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContext _unitOfWork;
        private IRepositoryCacheProvider _repositoryCacheProvider;

        public UnitOfWork(IDbContext unitOfWork, IRepositoryCacheProvider repositoryCacheProvider)
        {
            _unitOfWork = unitOfWork;
            _repositoryCacheProvider = repositoryCacheProvider;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public IRepository<T> GetRepository<T>() where T : class, IMaintainableEntity
        {
            return _repositoryCacheProvider.Get<T>(_unitOfWork);
        }

        public int SaveChanges()
        {
            return _unitOfWork.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
