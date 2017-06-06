using System.Collections.Generic;
using Licenta.EntityFramework.UnitOfWork.Interfaces;

namespace Licenta.EntityFramework.UnitOfWork
{
    public class RepositoryCacheProvider : IRepositoryCacheProvider
    {
        private List<IRepository> _repositories;

        public RepositoryCacheProvider()
        {
            _repositories = new List<IRepository>();
        }

        public IRepository<T> Get<T>(IDbContext context) where T : class, IMaintainableEntity
        {
            IRepository<T> returnableRepository;
            foreach (IRepository repository in _repositories)
            {
                returnableRepository = repository as IRepository<T>;
                if (returnableRepository != null)
                    return returnableRepository;
            }
            returnableRepository = new GenericRepository<T>(context);
            _repositories.Add(returnableRepository);
            return returnableRepository;
        }
    }
}
