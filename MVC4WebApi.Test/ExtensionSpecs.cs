using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC4WebApi.Domain;
using MVC4WebApi.Models;
using MVC4WebApi.Extensions;
using NUnit.Framework;
using System.Net.Http;

namespace MVC4WebApi.Test
{
    [TestFixture]
    public class ExtensionSpecs
    {
        Account domainAccount;
        AccountModel modelAccount;
        [SetUp]
        public void Setup()
        {
            domainAccount = new Account { Id = 1, AccountCode = "A001", Name = "Account A001", IsActive = true };
            modelAccount = new AccountModel { Version = 1.0, Id = 2, AccountCode = "B001", Name = "Account B001", IsActive = true };
        }

        [Test]
        public void canMapDomainAccountToModelAccount()
        {
            var model = domainAccount.MapModel(version: 1, request:null);
            Assert.AreEqual(model.Version, 1);
            Assert.AreEqual(model.Id, domainAccount.Id);
            Assert.AreEqual(model.AccountCode, domainAccount.AccountCode);
            Assert.AreEqual(model.Name, domainAccount.Name);
            Assert.AreEqual(model.IsActive, domainAccount.IsActive);
        }

        [Test]
        public void canMapModelAccountToDomainAccount()
        {
            var domain = modelAccount.MapDomain();
            Assert.AreEqual(domain.Id, modelAccount.Id);
            Assert.AreEqual(domain.AccountCode, modelAccount.AccountCode);
            Assert.AreEqual(domain.Name, modelAccount.Name);
            Assert.AreEqual(domain.IsActive, modelAccount.IsActive);
        }
    }
}
