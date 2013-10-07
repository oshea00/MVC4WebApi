using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using MVC4WebApi.Domain;
using MVC4WebApi.Models;
using MVC4WebApi.Extensions;

namespace MVC4WebApi.Controllers
{
    public class TokenController : BaseApiController
    {
        public HttpResponseMessage Post(TokenRequestModel tokenrequest)
        {
            try
            {
                // Lookup the token.ApiKey and verify it was issued
                // we'll fake it here - any key is good.

                // Simple naive check that secret matches ours
                var key = "sharedkey";
                var provider = new System.Security.Cryptography.HMACSHA256(Encoding.UTF8.GetBytes(key));
                var hash = provider.ComputeHash(Encoding.UTF8.GetBytes(tokenrequest.ApiKey));
                var signature = Convert.ToBase64String(hash);

                if (signature == tokenrequest.Signature)
                {
                    // Gen new token
                    var rawTokenInfo = string.Concat(tokenrequest.ApiKey + DateTime.UtcNow.ToString("d"));
                    var rawTokenByte = Encoding.UTF8.GetBytes(rawTokenInfo);
                    var token = provider.ComputeHash(rawTokenByte);

                    var authToken = new AuthToken
                    {
                        Token = Convert.ToBase64String(token),
                        Expiration = DateTime.UtcNow.AddDays(7),
                        ApiUser = tokenrequest.ApiKey,
                    };

                    // We could save this token for later checking...
                    // We probably want to cleanup expired tokens in our store too...
                    // later....

                    return Request.CreateResponse(HttpStatusCode.Created, authToken.MapModel());

                }
                else
                {
                    throw new Exception("Bad signature");
                }
            }
            catch (Exception ex) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
           
        }
    }
}