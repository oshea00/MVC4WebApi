using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace MVC4WebApi.Controllers
{
    public class BaseApiController : ApiController
    {
        public double Version { get; set; }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
 	         base.Initialize(controllerContext);
             Version = 1.0;
             var versionHeader = Request.Headers.FirstOrDefault(h => h.Key == "X-Version");
             if (versionHeader.Value != null)
             {
                 double version;
                 if (double.TryParse(versionHeader.Value.ToList()[0],out version))
                 {
                     Version = version;
                 }
             }
        }
    }
}
