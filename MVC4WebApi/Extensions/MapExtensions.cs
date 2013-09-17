using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC4WebApi.Models;

namespace MVC4WebApi.Extensions
{
    public static class MapExtensions
    {
        public static Account AccountMap(this MVC4WebApi.Domain.Account acct, int version)
        {
            if (acct == null)
                return null;


            return new Account
            {
                Version = version,
                Id = acct.Id,
                AccountCode = acct.AccountCode,
                Name = acct.Name,
                IsActive = acct.IsActive,
            };
        }
    }
}