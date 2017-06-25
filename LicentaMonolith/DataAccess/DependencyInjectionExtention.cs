using Contracts.DataAccess;
using DataAccess.Repositories;
using Microsoft.Practices.Unity;

namespace DataAccess
{
    public class DependencyInjectionExtention : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IUnitOfWork, UnitOfWork>(new PerThreadLifetimeManager());
            Container.RegisterType<IMonolithDbContext, MonolithDbContext>(new InjectionConstructor());
            Container.RegisterType<IRepositoryCacheProvider, RepositoryCacheProvider>();
        }
    }
}
