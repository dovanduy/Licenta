using Licenta.EntityFramework.DependencyInjection;
using Licenta.EntityFramework.UnitOfWork.Interfaces;
using Licenta.Review.EntityFramework;
using Licenta.Review.Services;
using Licenta.Review.Services.Interfaces;
using Microsoft.Practices.Unity;

namespace Licenta.Review
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
            container.RegisterType<IDbContext, ReviewDbContext>(new InjectionConstructor());
            container.RegisterType<IReviewService,ReviewService>();
        }
    }

    public class UnitOfWork : Licenta.EntityFramework.UnitOfWork.UnitOfWork
    {
        public UnitOfWork(IDbContext unitOfWork, IRepositoryCacheProvider repositoryCacheProvider) : base(unitOfWork, repositoryCacheProvider)
        {
        }
    }
}