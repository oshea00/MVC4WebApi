using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using MVC4WebApi.Domain;
using MVC4WebApi.Models;

namespace MVC4WebApi.Extensions
{
    public static class MapExtensions
    {
        public static string GetUrl(string routeName, HttpRequestMessage request, object parmObject)
        {
            string url = "";
            if (request != null)
            {
                try
                {
                    var urlHelper = new UrlHelper(request);
                    url = urlHelper.Link(routeName, parmObject);
                }
                catch { }
            }
            return url;
        }

        public static AuthTokenModel MapModel(this AuthToken auth)
        {
            return new AuthTokenModel
            {
                Expiration = auth.Expiration,
                Token = auth.Token
            };
        }

        public static AccountModel MapModel(this Account acct, double version, HttpRequestMessage request)
        {
            if (acct == null)
                return null;

            string url = GetUrl("Account", request, new { id = acct.Id });

            // Version 1 Account Model

            if (version == 1.0)
            {
                return new AccountModel
                {
                    Version = version,
                    Url = url,
                    Id = acct.Id,
                    AccountCode = acct.AccountCode,
                    Name = acct.Name,
                    IsActive = acct.IsActive,
                    AccountName = null,
                };
            }

            // Version 2 (Current) Account Model
            return new AccountModel
            {
                Version = version,
                Url = url,
                Id = acct.Id,
                AccountCode = acct.AccountCode,
                Name = null,
                IsActive = acct.IsActive,
                AccountName = acct.Name,
                Balance = acct.Balance,
                BalanceDate = acct.BalanceDate,

            };
        }

        public static Account MapDomain(this AccountModel acct)
        {
            if (acct == null)
                return null;

            // Version 1 Account Model

            if (acct.Version == 1.0)
            {
                return new Account
                {
                    Id = acct.Id,
                    AccountCode = acct.AccountCode,
                    Name = acct.Name,
                    IsActive = acct.IsActive,
                };
            }

            // Version 2 (Current) Account Model
            return new Account
            {
                Id = acct.Id,
                AccountCode = acct.AccountCode,
                Name = acct.AccountName,
                IsActive = acct.IsActive,
                Balance = acct.Balance,
                BalanceDate = acct.BalanceDate,
            };
        }
    }
}