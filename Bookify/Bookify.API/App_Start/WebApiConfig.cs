using System.Web.Http;

namespace Bookify.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{method}/{id}",
                defaults: new
                {
                    id = RouteParameter.Optional,
                    method = "Get"
                }
            );

            // return output as json
            config.Formatters.Add(new BrowserJsonFormatter());
        }
    }
}
