using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace MVC4WebApi.Controllers
{
    public class CustomControllerSelector : DefaultHttpControllerSelector
    {
        private HttpConfiguration _config;
        public CustomControllerSelector(HttpConfiguration config) : base(config)
        {
            _config = config;
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            // get controllers
            var controllers = GetControllerMapping();
            // get route data
            var routeData = request.GetRouteData();
            // get controller named on route
            var controllerName = (string) routeData.Values["controller"];
            // setup descriptor
            HttpControllerDescriptor descriptor;

            if (controllers.TryGetValue(controllerName, out descriptor))
            {
                var version = "2";
                // we can change the controller we want to get here...
                var newName = string.Concat(controllerName, "V", version);
                HttpControllerDescriptor versionedDescriptor;
                if (controllers.TryGetValue(newName, out versionedDescriptor))
                {
                    return versionedDescriptor;
                }
                // default one based on route if we haven't found our
                // alternative controller.
                return descriptor; 
            }

            return null; // haven't found one
        }
    }
}