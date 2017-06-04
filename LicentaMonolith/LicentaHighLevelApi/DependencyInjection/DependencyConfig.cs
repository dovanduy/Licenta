using System.Web.Http;
using BusinessLogic;
using Microsoft.Practices.Unity;

namespace LicentaHighLevelApi.DependencyInjection
{
    public class DependencyConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();

            ApplicationContainerConfiguration.Register(container);

            config.DependencyResolver = new UnityResolver(container);
        }
    }
}