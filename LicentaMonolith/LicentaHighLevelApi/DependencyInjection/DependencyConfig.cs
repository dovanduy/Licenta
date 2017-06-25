using System.Web.Http;
using BusinessLogic;
using Microsoft.Practices.Unity;

namespace LicentaMonolithHighLevelApi.DependencyInjection
{
    public class DependencyConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();

            container.AddNewExtension<DependencyInjectionExtention>();

            config.DependencyResolver = new UnityResolver(container);
        }
    }
}