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

        public IEnumerable<Account> Get()
        {
            foreach (var acct in _accountRepo.GetAll())
            {
                yield return acct.AccountMap(Version);
            }
        }

        public Account Get(int id)
        {
            return _accountRepo.Get(id).AccountMap(Version);
        }

        public HttpResponseMessage PostAccount(Account account)
        {
            if (account.Id > 0)
            {
                if (!_accountRepo.Update(account.AccountMap(Version)))
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                var response = Request.CreateResponse<Account>(HttpStatusCode.OK,account);
                string uri = Url.Link("DefaultApi", new { id = account.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            else
            {
                var domainAccount = account.AccountMap(Version);
                var newDomainAccount = _accountRepo.Add(domainAccount);
                var newAccount = newDomainAccount.AccountMap(Version);
                var response = Request.CreateResponse<Account>(HttpStatusCode.Created, newAccount);
                string uri = Url.Link("DefaultApi", new { id = newAccount.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
        }

        public void Delete(int id)
        {
            MVC4WebApi.Domain.Account account = _accountRepo.Get(id);
            if (account == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            _accountRepo.Delete(account.Id);
        }
    }
}

