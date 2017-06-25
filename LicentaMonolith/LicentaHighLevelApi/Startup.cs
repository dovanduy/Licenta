using System.Web.Http;
using LicentaMonolithHighLevelApi;
using LicentaMonolithHighLevelApi.AppStart;
using LicentaMonolithHighLevelApi.DependencyInjection;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace LicentaMonolithHighLevelApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            WebApiConfig.Register(config);
            SwaggerConfig.Register(config);
            DependencyConfig.Register(config);

            AuthZeroConfig.ConfigureAuthZero(app);

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }
    }
}