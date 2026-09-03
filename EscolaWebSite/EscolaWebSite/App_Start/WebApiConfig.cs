using System.Web.Http;
using System.Web.UI.WebControls;

namespace EscolaWebSite
{
    public static class WebApiConfig
    {
        public static void Register(System.Web.Http.HttpConfiguration config)
        {

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