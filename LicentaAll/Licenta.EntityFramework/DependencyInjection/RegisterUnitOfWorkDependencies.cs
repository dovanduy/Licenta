using Licenta.EntityFramework.UnitOfWork;
using Licenta.EntityFramework.UnitOfWork.Interfaces;
using Microsoft.Practices.Unity;

namespace Licenta.EntityFramework.DependencyInjection
{
    public static class RegisterUnitOfWorkDependencies
    {
        public static void InContainer(IUnityContainer container)
        {
            //container.RegisterType<IUnitOfWork, UnitOfWork.UnitOfWork>(new PerThreadLifetimeManager());
            container.RegisterType<IRepositoryCacheProvider,RepositoryCacheProvider>();
        }
    }
}
