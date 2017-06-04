using LicentaHighLevelApi;
using LicentaHighLevelApi.AppStart;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System.Web.Http;
using LicentaHighLevelApi.DependencyInjection;

[assembly: OwinStartup(typeof(Startup))]
namespace LicentaHighLevelApi
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