using Microsoft.AspNetCore.Cors;
using System.Web.Http;

namespace EscolaWebSite.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Enable CORS
            // var cors = new EnableCorsAttribute("*", "*", "*");
            // config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Remove XML formatter, use JSON only
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}