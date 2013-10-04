using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MVC4WebApi.Domain.Data;
using MVC4WebApi.Extensions;
using MVC4WebApi.Models;

namespace MVC4WebApi.Controllers
{
    public class AccountController : BaseApiController
    {
        IAccountRepo _accountRepo;

        public AccountController(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public IEnumerable<AccountModel> Get()
        {
            foreach (var acct in _accountRepo.GetAll())
            {
                yield return acct.MapModel(Version,Request);
            }
        }

        public IEnumerable<AccountStatsModel> GetStats(bool stats)
        {
            double bal=0;
            foreach (var acct in _accountRepo.GetAll())
            {
                bal += acct.Balance;
            }
            return new List<AccountStatsModel> { new AccountStatsModel { Version = 2.0, Count = _accountRepo.Count(), TotalBalance = bal }, } ;
        }

        public IEnumerable<AccountModel> Get(int page, int pageSize)
        {
            foreach (var acct in _accountRepo.GetPage(page,pageSize))
            {
                yield return acct.MapModel(Version,Request);
            }
        }

        public IEnumerable<AccountModel> GetOrderBy(int page, int pageSize, string orderBy)
        {
            IList<AccountModel> all = null;
            try
            {
                if (orderBy.First() == '-')
                {
                    all = Get().OrderByDescending(p => p.GetType().GetProperty(orderBy.Substring(1)).GetValue(p, null)).ToList();
                }
                else
                {
                    all = Get().OrderBy(p => p.GetType().GetProperty(orderBy).GetValue(p, null)).ToList();
                }
            }
            catch
            {
                all = new List<AccountModel>();
            }
            return all.Skip(page * pageSize).Take(pageSize);
        }

        public AccountModel Get(int id)
        {
            return _accountRepo.Get(id).MapModel(Version, Request);
        }

        public HttpResponseMessage PostAccount(AccountModel accountModel)
        {
            if (accountModel.Id > 0)
            {
                if (!_accountRepo.Update(accountModel.MapDomain()))
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                return Request.CreateResponse<AccountModel>(HttpStatusCode.OK,accountModel);
            }
            else
            {
                var newAccount = _accountRepo.Add(accountModel.MapDomain());
                var newAccountModel = newAccount.MapModel(Version,Request);
                return Request.CreateResponse<AccountModel>(HttpStatusCode.Created, newAccountModel);
            }
        }

        public void Delete(int id)
        {
            _accountRepo.Delete(id);
        }

        public IEnumerable<AccountModel> GetBySearch(string search)
        {
            if (search == null)
                yield break;

            var s = search.ToLower();
            var all = _accountRepo.GetAll();
            
            foreach (var am in all.Where(a =>
                a.AccountCode.ToLower().Contains(s) ||
                a.Name.ToLower().Contains(s) ||
                a.IsActive.ToString().ToLower().Contains(s) ||
                a.Balance.ToString("f2").ToLower().Contains(s) ||
                a.BalanceDate.ToString("M/d/yyyy").Contains(s) ||
                a.BalanceDate.ToString("MM/dd/yyyy").Contains(s)
                ).ToList())
                {
                    yield return am.MapModel(Version, Request);                
                }
        }
    }
}

