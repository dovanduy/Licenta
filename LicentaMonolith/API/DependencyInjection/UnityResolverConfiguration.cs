using System.Web.Http;
using Microsoft.Practices.Unity;

namespace API.DependencyInjection
{
    public class UnityResolverConfiguration
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();

            BusinessLogic.ApplicationContainerConfiguration.Register(container);

            config.DependencyResolver = new UnityResolver(container);
        }
    }
}