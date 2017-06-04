using System.Collections.Generic;
using Contracts.DataAccess;

namespace DataAccess.Repositories
{
    public class RepositoryCacheProvider : IRepositoryCacheProvider
    {
        private List<IRepository> _repositories;

        public RepositoryCacheProvider()
        {
            _repositories = new List<IRepository>();
        }

        public IRepository<T> Get<T>(IDbContext context) where T:class,IMaintainableEntity
        {
            IRepository<T> returnableRepository;
            foreach (IRepository repository in _repositories)
            {
                returnableRepository = repository as IRepository<T>;
                if (returnableRepository != null)
                    return returnableRepository;
            }
            returnableRepository = new GenericRepository<T>( (MonolithDbContext) context);
            _repositories.Add(returnableRepository);
            return returnableRepository;
        }
    }
}
