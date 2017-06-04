using Contracts.DataAccess;
using DataAccess;
using DataAccess.Repositories;
using Microsoft.Practices.Unity;

namespace DependencyInjection
{
    public class ApplicationContainerConfiguration
    {
        public static void Register(IUnityContainer container)
        {
            container.RegisterType<IRepository<Product>, ProductRepository>();
            container.RegisterType<IRepository<Category>, CategoryRepository>();
            container.RegisterType<IRepository<Review>, ReviewRepository>();
        }
    }
}
