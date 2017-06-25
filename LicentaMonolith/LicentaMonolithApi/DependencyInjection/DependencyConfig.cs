using System.Web.Http;
using BusinessLogic;
using Microsoft.Practices.Unity;

namespace LicentaMonolithApi.DependencyInjection
{
    public static class DependencyConfig
    {
        public static UnityResolver GetContainer()
        {
            var container = new UnityContainer();

            container.AddNewExtension<DependencyInjectionExtention>();

            return new UnityResolver(container);
        }
    }
}