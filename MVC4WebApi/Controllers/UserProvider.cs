using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MVC4WebApi.Controllers
{
    public interface IUserProvider
    {
        string UserName { get; }
    }

    public class UserProvider : IUserProvider
    {
        public string UserName
        {
            get
            {
#if !DEBUG
                return Thread.CurrentPrincipal.Identity.Name;
#else
                return "mikeo";
#endif
            }
        }
    }



}
