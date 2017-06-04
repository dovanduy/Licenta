using Contracts.DataAccess;

namespace DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IMonolithDbContext _unitOfWork;
        private IRepositoryCacheProvider _repositoryCacheProvider;

        public UnitOfWork(IMonolithDbContext unitOfWork, IRepositoryCacheProvider repositoryCacheProvider)
        {
            _unitOfWork = unitOfWork;
            _repositoryCacheProvider = repositoryCacheProvider;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public IRepository<T> GetRepository<T>() where T : class,IMaintainableEntity
        {
            return _repositoryCacheProvider.Get<T>(_unitOfWork);
        }

        public void SaveChanges()
        {
            _unitOfWork.SaveChanges();
        }
    }
}
