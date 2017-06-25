using System.Web.Http;
using LicentaMonolithApi;
using LicentaMonolithApi.AppStart;
using LicentaMonolithApi.DependencyInjection;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace LicentaMonolithApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            WebApiConfig.Register(config);
            SwaggerConfig.Register(config);
            config.DependencyResolver = DependencyConfig.GetContainer();

            AuthZeroConfig.ConfigureAuthZero(app);

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }
    }
}