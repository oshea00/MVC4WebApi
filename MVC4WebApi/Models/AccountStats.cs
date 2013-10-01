using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC4WebApi.Models
{
    public class AccountStats : ModelBase
    {
        public double TotalBalance { get; set; }
        public int Count { get; set; }
    }
}
