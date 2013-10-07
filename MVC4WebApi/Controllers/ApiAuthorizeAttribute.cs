using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Security.Principal;
using System.Threading;
using MVC4WebApi.Domain;

namespace MVC4WebApi.Controllers
{
    public class ApiAuthorizeAttribute : AuthorizationFilterAttribute
    {
        private bool _perUser;
        public ApiAuthorizeAttribute(bool perUser = true)
        {
            _perUser = perUser;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            const string APIKEYNAME = "apikey";
            const string TOKENNAME = "token";

            var query = HttpUtility.ParseQueryString(actionContext.Request.RequestUri.Query);

            if (!string.IsNullOrWhiteSpace(query[APIKEYNAME]) &&
                !string.IsNullOrWhiteSpace(query[TOKENNAME]))
            {

                var apikey = query[APIKEYNAME];
                var token = query[TOKENNAME];

                // Lookup token in a repo - but we'll just copy it here from literal
                var authtoken = new AuthToken { ApiUser = apikey, Token = token, Expiration = DateTime.UtcNow.AddDays(7), };

                // 
                if (authtoken != null && authtoken.ApiUser == apikey && authtoken.Expiration > DateTime.UtcNow)
                {

                    if (_perUser)
                    {
                        // If we are already authenticated no need to re-check authorization header
                        if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
                        {
                            return;
                        }

                        // Check authorization header
                        var authHeader = actionContext.Request.Headers.Authorization;
                        if (authHeader != null)
                        {
                            if (authHeader.Scheme.Equals("basic", StringComparison.CurrentCultureIgnoreCase) &&
                                !string.IsNullOrWhiteSpace(authHeader.Parameter))
                            {
                                var rawCredentials = authHeader.Parameter;
                                var encoding = Encoding.GetEncoding("iso-8859-1");
                                var credentials = encoding.GetString(Convert.FromBase64String(rawCredentials));
                                // parse
                                var split = credentials.Split(':');  // assumes : not a valid character
                                var username = split[0];
                                var password = split[1]; // assume we're using ssl so no explicit decryption is needed

                                // Validate credentials - we could use the built-in forms auth WebMatrix.WebData.WebSecurity.
                                // In this case, we're just going to fake it. as long as the credentials are non-zero length...
                                // we'll just set the user as current principal.
                                if (username.Length > 0 && password.Length > 0)
                                {
                                    var principal = new GenericPrincipal(new GenericIdentity(username), null);
                                    Thread.CurrentPrincipal = principal;
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        return;
                    }

                }
            }


            HandleUnauthorized(actionContext);
        }

        void HandleUnauthorized(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            // Optional header example
            actionContext.Response.Headers.Add("WWW-Authenticate", "Basic Scheme='MVC4WebApi' location='https://localhost:44300/login'");
        }
    }
}