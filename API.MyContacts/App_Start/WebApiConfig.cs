using API.MyContacts.Filters;
using System.Web.Http;

namespace API.MyContacts
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new ExceptionHandlerFilterAttribute());

            //config.Filters.Add(new RequestValidationFilterAttribute());
           
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            // config.MessageHandlers.Add(new ValidationHandler());
        }
    }
}
