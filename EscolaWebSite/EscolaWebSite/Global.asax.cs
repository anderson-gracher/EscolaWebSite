using EscolaWebSite.App_Start;
using System;
using System.Web.Http;

namespace EscolaWebSite
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Register Unity
            UnityConfig.RegisterComponents();

            // Register Web API
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}