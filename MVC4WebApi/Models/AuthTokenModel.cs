using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC4WebApi.Models
{
    public class AuthTokenModel
    {
        public DateTime Expiration { get; set; }
        public string Token { get; set; }
    }
}