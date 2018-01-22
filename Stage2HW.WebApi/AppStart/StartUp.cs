using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using Stage2HW.WebApi.Bootstrap;

namespace Stage2HW.WebApi.AppStart
{
    internal class StartUp
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.EnableCors(new EnableCorsAttribute("*", "*", "*", "X-Custom-Header"));

            config.Routes.MapHttpRoute(
                name: "ControllerOnly",
                routeTemplate: "Exchange/{controller}"
            );

            config.Routes.MapHttpRoute(
                name: "ControllerAndId",
                routeTemplate: "Exchange/{controller}/{id}",
                defaults: null,
                constraints: new { id = @"^\d+$" }
            );
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            appBuilder.UseNinjectMiddleware(NinjectBootstrap.GetKernel).UseNinjectWebApi(config);

            appBuilder.UseWebApi(config);
        }
    }
}