using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC4WebApi.Domain;
using MVC4WebApi.Extensions;
using NUnit.Framework;

namespace MVC4WebApi.Test
{
    [TestFixture]
    public class ExtensionSpecs
    {
        Account account;
        [SetUp]
        public void Setup()
        {
            account = new Account { Id = 1, AccountCode = "A001", Name = "Account A001", IsActive = true };
        }

        [Test]
        public void canMapDomainAccountToModelAccount()
        {
            var model = account.AccountMap(version: 1);
            Assert.AreEqual(model.Version, 1);
            Assert.AreEqual(model.Id, account.Id);
            Assert.AreEqual(model.AccountCode, account.AccountCode);
            Assert.AreEqual(model.Name, account.Name);
            Assert.AreEqual(model.IsActive, account.IsActive);
        }
    }
}
