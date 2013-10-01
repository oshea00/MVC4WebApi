using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC4WebApi.Domain.Data
{
    public interface IAccountRepo
    {
        IEnumerable<Account> GetAll();
        IEnumerable<Account> GetPage(int page, int pageSize);
        Account Get(int id);
        int Count();
        Account Add(Account account);
        bool Update(Account account);
        void Delete(int id);
    }

    public class AccountRepo : IAccountRepo
    {
        List<Account> _accounts;

        public AccountRepo()
        {
            var rand = new Random(1000);
            //var rand = new Random((int)DateTime.Now.Ticks);
            DateTime balDate = DateTime.Today;
            _accounts = new List<Account>();
            for (var i = 0; i < 100; i++)
            {
                var acct = new Account { 
                    Id = (i + 1), 
                    AccountCode = "A" + String.Format("{0:D4}", i), 
                    Name = "Account " + i, 
                    IsActive = (i % 10) != 0, 
                    Balance = (rand.Next(1,11)*1000.00), 
                    BalanceDate = balDate 
                };
                _accounts.Add(acct);
            }
        }

        public int Count()
        {
            return _accounts.Count();
        }

        public IEnumerable<Account> GetAll()
        {
            foreach (var acct in _accounts)
            {
                yield return acct;
            }
        }

        public IEnumerable<Account> GetPage(int page, int pageSize)
        {
            var maxIdx = _accounts.Count();
            for (var i = page * pageSize; i < (page + 1) * pageSize; i++)
            {
                if ( i < maxIdx)
                    yield return _accounts[i];
            }
        }

        public Account Get(int id)
        {
            return _accounts.FirstOrDefault(x => x.Id == id);
        }


        public Account Add(Account account)
        {
            // add to repository and return assigned Id
            var id = _accounts.Count();
            account.Id = id+1;
            _accounts.Add(account);
            return account;
        }

        public bool Update(Account account)
        {
            var acct = _accounts.FirstOrDefault(a => a.Id == account.Id);
            if (acct != null)
            {
                _accounts[account.Id - 1] = account;
                return true;
            }
            return false;
        }

        public void Delete(int id)
        {
            var account = _accounts.FirstOrDefault(a=>a.Id==id);
            if (account != null)
            {
                _accounts.Remove(account);
            }
        }
    }
}

