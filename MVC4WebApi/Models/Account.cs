using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC4WebApi.Models
{
    public class Account : ModelBase
    {
        public int Id { get; set; }
        public string AccountCode { get; set; }
        public string Name { get; set; }
        public string AccountName { get; set; }
        public bool IsActive { get; set; }
    }
}