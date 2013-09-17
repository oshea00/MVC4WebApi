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
            Assert.Greater(accountRepo.Save(new Account { Id = 0, AccountCode = "A01" }), 0);
        }

        [Test]
        public void canUpdateAnAccount()
        {
            Assert.AreEqual(1,accountRepo.Save(new Account { Id = 1, AccountCode = "A01", Name = "Account A01 Updated", IsActive = true }));
            var account = accountRepo.getById(1);
            Assert.AreEqual(account.Name, "Account A01 Updated");
        }

        [Test]
        public void returnsZeroIfCannotpdateAnAccount()
        {
            Assert.AreEqual(0, accountRepo.Save(new Account { Id = 9000, AccountCode = "A01", Name = "Account A01 Updated", IsActive = true }));
        }


    }
}
