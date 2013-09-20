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
        Account domainAccount;
        MVC4WebApi.Models.Account modelAccount;
        [SetUp]
        public void Setup()
        {
            domainAccount = new Account { Id = 1, AccountCode = "A001", Name = "Account A001", IsActive = true };
            modelAccount = new MVC4WebApi.Models.Account { Id = 2, AccountCode = "B001", Name = "Account B001", IsActive = true };
        }

        [Test]
        public void canMapDomainAccountToModelAccount()
        {
            var model = domainAccount.AccountMap(version: 1);
            Assert.AreEqual(model.Version, 1);
            Assert.AreEqual(model.Id, domainAccount.Id);
            Assert.AreEqual(model.AccountCode, domainAccount.AccountCode);
            Assert.AreEqual(model.Name, domainAccount.Name);
            Assert.AreEqual(model.IsActive, domainAccount.IsActive);
        }

        [Test]
        public void canMapModelAccountToDomainAccount()
        {
            var domain = modelAccount.AccountMap(version: 1);
            Assert.AreEqual(domain.Id, modelAccount.Id);
            Assert.AreEqual(domain.AccountCode, modelAccount.AccountCode);
            Assert.AreEqual(domain.Name, modelAccount.Name);
            Assert.AreEqual(domain.IsActive, modelAccount.IsActive);
        }
    }
}
