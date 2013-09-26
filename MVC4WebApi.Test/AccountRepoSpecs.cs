using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC4WebApi.Domain;
using MVC4WebApi.Domain.Data;
using NUnit.Framework;

namespace MVC4WebApi.Test
{
    [TestFixture]
    public class AccountRepoSpecs
    {
        AccountRepo accountRepo;

        [SetUp]
        public void Setup()
        {
            accountRepo = new AccountRepo();
        }

        [Test]
        public void canSaveAnAccount()
        {
            Assert.Greater(accountRepo.Add(new Account { Id = 0, AccountCode = "A01" }).Id, 0);
        }

        [Test]
        public void canUpdateAnAccount()
        {
            Assert.AreEqual(true,accountRepo.Update(new Account { Id = 1, AccountCode = "A01", Name = "Account A01 Updated", IsActive = true }));
            var account = accountRepo.Get(1);
            Assert.AreEqual(account.Name, "Account A01 Updated");
        }

        [Test]
        public void returnsFalseIfUpdatingNonExistentAccount()
        {
            Assert.AreEqual(false, accountRepo.Update(new Account { Id = -1, AccountCode = "A01", Name = "Account A01 Updated", IsActive = true }));
        }

        [Test]
        public void CanGetAllAccounts()
        {
            Assert.Greater(accountRepo.GetAll().Count(), 0);
        }

        [Test]
        public void CanDeleteExistingAccount()
        {
            var account = accountRepo.Add(new Account { Id = 0, AccountCode = "A01" });
            accountRepo.Delete(account.Id);
            Assert.IsNull(accountRepo.Get(account.Id));
        }

        [Test]
        public void CanDeleteNonExistingAccount()
        {
            accountRepo.Delete(-1);
            Assert.IsNull(accountRepo.Get(-1));
        }


    }
}
