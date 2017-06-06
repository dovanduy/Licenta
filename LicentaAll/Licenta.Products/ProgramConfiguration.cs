using Licenta.EntityFramework.DependencyInjection;
using Licenta.EntityFramework.UnitOfWork.Interfaces;
using Licenta.Products.Services;
using Licenta.Products.Services.Interfaces;
using Microsoft.Practices.Unity;

namespace Licenta.Products
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
            container.RegisterType<IDbContext, ProductsDbContext>(new InjectionConstructor());
            container.RegisterType<IProductService,ProductService>();
            container.RegisterType<ICategoryService,CategoryService>();
        }
    }

    public class UnitOfWork : EntityFramework.UnitOfWork.UnitOfWork
    {
        public UnitOfWork(IDbContext unitOfWork, IRepositoryCacheProvider repositoryCacheProvider) : base(unitOfWork, repositoryCacheProvider)
        {
        }
    }
}