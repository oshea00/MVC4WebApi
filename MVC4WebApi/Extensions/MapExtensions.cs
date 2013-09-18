using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC4WebApi.Models;

namespace MVC4WebApi.Extensions
{
    public static class MapExtensions
    {
        public static Account AccountMap(this MVC4WebApi.Domain.Account acct, double version)
        {
            if (acct == null)
                return null;

            // Version 1 Account Model

            if (version == 1.0)
            {
                return new Account
                {
                    Version = version,
                    Id = acct.Id,
                    AccountCode = acct.AccountCode,
                    Name = acct.Name,
                    IsActive = acct.IsActive,
                    AccountName = null,
                };
            }

            // Version 2 (Current) Account Model
            return new Account
            {
                Version = version,
                Id = acct.Id,
                AccountCode = "V2" + acct.AccountCode,
                Name = null,
                IsActive = acct.IsActive,
                AccountName = acct.Name,

            };
        }
    }
}