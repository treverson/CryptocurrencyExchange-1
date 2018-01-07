using System.Net.Http.Headers;
using System.Web.Http;
using Owin;

namespace Stage2HW.WebApi
{
    internal class StartUp
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "CurrencyExchangeApi",
                routeTemplate: "Exchange/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            appBuilder.UseWebApi(config);
        }
    }
}