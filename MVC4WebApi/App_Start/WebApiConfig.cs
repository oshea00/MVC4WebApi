using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using MVC4WebApi.Controllers;
using MVC4WebApi.Extensions;
using WebApiContrib.Formatting.Jsonp;

namespace MVC4WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "Account",
                routeTemplate: "api/Account/{id}",
                defaults: new { controller = "Account", id = RouteParameter.Optional }
            );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();
            config.MapHttpAttributeRoutes();

            // Use our own controller selector
            config.Services.Replace(typeof(IHttpControllerSelector), new CustomControllerSelector(config));

            // Get json formatter in case we want to configure it.
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            
            // Get jsonp formatter and add to configuration
            var jsonpFormatter = new JsonpMediaTypeFormatter(jsonFormatter);
            config.Formatters.Insert(0, jsonpFormatter);
            
            // WebApi2 CORs
            // config.EnablCORS

            // Add in our require https filter for WebApi so it applies to all Api controllers
#if !DEBUG
            config.Filters.Add(new ApiRequireHttpsAttribute());
#endif
        }
    }
}
