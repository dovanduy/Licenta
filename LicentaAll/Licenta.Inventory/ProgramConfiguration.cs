using Licenta.EntityFramework.DependencyInjection;
using Licenta.EntityFramework.UnitOfWork.Interfaces;
using Microsoft.Practices.Unity;

namespace Licenta.Inventory
{
    internal partial class Program
    {
        static IUnityContainer CreateContainer()
        {
            UnityContainer container = new UnityContainer();

            RegisterInContainer(container);
            RegisterUnitOfWorkDependencies.InContainer(container);

            return container;
        }

        static void RegisterInContainer(IUnityContainer container)
        {
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerThreadLifetimeManager());
            container.RegisterType<IDbContext, InventoryDbContext>(new InjectionConstructor());
        }
    }

    public class UnitOfWork : EntityFramework.UnitOfWork.UnitOfWork
    {
        public UnitOfWork(IDbContext unitOfWork, IRepositoryCacheProvider repositoryCacheProvider) : base(unitOfWork, repositoryCacheProvider)
        {
        }
    }
}