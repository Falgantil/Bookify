using System.Web.Http;
using Newtonsoft.Json;

namespace Bookify.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new
                {
                    id = RouteParameter.Optional,
                    action = "Get"
                }
            );

            // return output as json
            config.Formatters.Add(new BrowserJsonFormatter());
            
        }
    }
}
