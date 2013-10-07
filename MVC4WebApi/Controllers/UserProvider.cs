using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                return "mikeo";
            }
        }
    }



}
