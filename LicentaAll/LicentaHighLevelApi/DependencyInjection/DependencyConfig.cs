using LicentaHighLevelApi.Services;
using LicentaHighLevelApi.Services.Interfaces;
using Microsoft.Practices.Unity;
using System.Web.Http;

namespace LicentaHighLevelApi.AppStart
{
    public class DependencyConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();

            container.RegisterType<IProductsService, ProductsService>();

            config.DependencyResolver = new UnityResolver(container);
        }
    }
}