using Contracts.DataAccess;
using DataAccess.Repositories;
using Microsoft.Practices.Unity;

namespace DataAccess
{
    public class ApplicationContainerConfiguration
    {
        public static void Register(IUnityContainer container)
        {
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerThreadLifetimeManager());
            container.RegisterType<IMonolithDbContext, MonolithDbContext>();
            container.RegisterType<IRepositoryCacheProvider, RepositoryCacheProvider>();
        }
    }
}
