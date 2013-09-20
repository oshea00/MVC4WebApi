﻿using System;
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
            foreach (var acct in _accountRepo.getAll())
            {
                yield return acct.AccountMap(Version);
            }
        }

        public Account Get(int id)
        {
            return _accountRepo.getById(id).AccountMap(Version);
        }

        public void Post([FromBody]Account account)
        {
            // Add
        }

        public void Put(int id, [FromBody]Account account)
        {
            // Update
        }

        public void Delete(int id)
        {

        }
    }
}

