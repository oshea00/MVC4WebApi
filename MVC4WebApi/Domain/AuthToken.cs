using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC4WebApi.Domain
{
    public class AuthToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string ApiUser { get; set; }
    }
}