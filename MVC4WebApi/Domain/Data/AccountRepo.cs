using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC4WebApi.Domain.Data
{
    public interface IAccountRepo
    {
        IEnumerable<Account> getAll();
        Account getById(int id);
        int Save(Account account);
    }

    public class AccountRepo : IAccountRepo
    {
        List<Account> _accounts;

        public AccountRepo()
        {
            _accounts = new List<Account>();
            for (var i = 0; i < 1000; i++)
            {
                var acct = new Account { Id = (i + 1), AccountCode = "A" + String.Format("{0:D4}", i), Name = "Account " + i, IsActive = (i % 10) != 0 };
                _accounts.Add(acct);
            }
        }

        public IEnumerable<Account> getAll()
        {
            foreach (var acct in _accounts)
            {
                yield return acct;
            }
        }

        public Account getById(int id)
        {
            return _accounts.FirstOrDefault(x => x.Id == id);
        }


        public int Save(Account account)
        {
            if (account.Id == 0)
            {
                // add to repository and return assigned Id
                var id = _accounts.Count();
                account.Id = id;
                _accounts.Add(account);
                return id;
            }
            else
            {
                var acct = _accounts.FirstOrDefault(a => a.Id == account.Id);
                if (acct != null)
                {
                    _accounts[account.Id - 1] = account;
                    return account.Id;
                }

                // update existing Id (if it exists) and return Id
            }
            return 0;
        }
    }
}

