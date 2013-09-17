using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC4WebApi.Domain
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountCode { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
