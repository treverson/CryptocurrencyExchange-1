using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using Stage2HW.Business.Services;
using Stage2HW.Business.Services.Interfaces;
using Stage2HW.Cli.Services;
using Stage2HW.Cli.Services.Interfaces;
using Stage2HW.WebApi.Bootstrap;
using Stage2HW.WebApi.Configuration;

namespace Stage2HW.WebApi.AppStart
{
    internal class StartUp
    {

        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.EnableCors(new EnableCorsAttribute("*", "*", "*", "X-Custom-Header"));

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ControllerOnly",
                routeTemplate: "Exchange/{controller}"
            );

            config.Routes.MapHttpRoute(
                name: "ControllerAndId",
                routeTemplate: "Exchange/{controller}/{id}"
              //  defaults: null,
               // constraints: new { id = @"^\d+$" }
            );

            config.Routes.MapHttpRoute(
                name: "ControllerIdAction",

                routeTemplate: "Exchange/{controller}/{id}/{action}"
            );


            config.Routes.MapHttpRoute(
                name: "ControllerAndAction",

                routeTemplate: "Exchange/{controller}/{action}"
            );

            config.Services.Replace(typeof(IExceptionHandler), new MyExceptionHandler());

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            appBuilder.UseNinjectMiddleware(NinjectBootstrap.GetKernel).UseNinjectWebApi(config);

            appBuilder.UseWebApi(config);
        }

        public class MyExceptionHandler : ExceptionHandler
        {
            public override void Handle(ExceptionHandlerContext context)
            {
                context.Result = new InternalServerErrorResult(context.Request);
            }
        }
    }
}