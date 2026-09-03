using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.UI.WebControls;

namespace EscolaGenerica
{
    public static class WebApiConfig
    {
        public static void Register(System.Web.Http.HttpConfiguration config)
        {
            // Enable CORS
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = System.Web.Http.RouteParameter.Optional }
            );

            // Remove XML formatter, use JSON only
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}