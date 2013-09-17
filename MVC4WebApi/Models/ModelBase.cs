using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC4WebApi.Models
{
    public abstract class ModelBase
    {
        public int Version { get; set; }
    }
}