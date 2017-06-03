using System.Web.Http;
using LicentaHighLevelApi.Services;
using LicentaHighLevelApi.Services.Interfaces;
using Microsoft.Practices.Unity;

namespace LicentaHighLevelApi.DependencyInjection
{
    public class DependencyConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();

            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<IReviewService, ReviewService>();

            config.DependencyResolver = new UnityResolver(container);
        }
    }
}