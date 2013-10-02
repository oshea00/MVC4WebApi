using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC4WebApi.Models
{
    public abstract class ModelBase
    {
        public double Version { get; set; }
        public string Url { get; set; }
    }
}