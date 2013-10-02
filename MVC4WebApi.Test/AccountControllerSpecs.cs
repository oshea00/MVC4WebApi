using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC4WebApi.Controllers;
using MVC4WebApi.Domain.Data;
using MVC4WebApi.Domain;
using MVC4WebApi.Extensions;
using NSubstitute;
using NUnit.Framework;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using MVC4WebApi.Models;

namespace MVC4WebApi.Test
{

    [TestFixture]
    public class AccountControllerSaveandUpdateGoodPathSpecs
    {
        IAccountRepo accountRepo;
        AccountController controller;
        AccountModel existingModelAccount;
        AccountModel newModelAccount;

        [SetUp]
        public void Setup()
        {
            accountRepo = Substitute.For<IAccountRepo>();

            accountRepo.GetAll().Returns(new List<Account> { 
                 new Account { Id = 1},
            });

            existingModelAccount = new AccountModel { Version = 2.0, Id = 1, AccountCode = "U10101", AccountName = "Updated Account", IsActive = true };
            newModelAccount = new AccountModel { Version = 2.0, Id = 0, AccountCode = "N10101", AccountName = "New Account", IsActive = true };

            accountRepo.Get(1).Returns(existingModelAccount.MapDomain());
            accountRepo.Add(Arg.Any<Account>()).Returns(newModelAccount.MapDomain());
            accountRepo.Update(Arg.Any<Account>()).Returns(true);

            controller = new AccountController(accountRepo);
            controller.Request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/Account");
            controller.Configuration = GlobalConfiguration.Configuration;
            controller.Version = 2.0;
        }

        [Test]
        public void returnsAllAccounts()
        {
            Assert.AreEqual(controller.Get().Count(), 1);
        }

        [Test]
        public void returnsAnAccount()
        {
            var account = controller.Get(1);
            Assert.IsNotNull(account);
        }

        [Test]
        public void savesAnUpdatedAccount()
        {
            var response = controller.PostAccount(existingModelAccount);
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [Test]
        public void savesAnNewAccount()
        {
            var response = controller.PostAccount(newModelAccount);
            Assert.IsTrue(response.IsSuccessStatusCode);
        }
    }


    [TestFixture]
    public class AccountControllerSaveandUpdateBadPathSpecs
    {
        IAccountRepo accountRepo;
        AccountController controller;

        [SetUp]
        public void Setup()
        {
            accountRepo = Substitute.For<IAccountRepo>();

            accountRepo.Update(Arg.Any<Account>()).Returns(false);

            controller = new AccountController(accountRepo);
            controller.Request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/Account");
            controller.Configuration = GlobalConfiguration.Configuration;
            controller.Version = 2.0;
        }

        [Test]
        public void UpdatingNonExistentAccountIsAnError()
        {
            Assert.Throws<HttpResponseException>(() =>
            {
                controller.PostAccount(new AccountModel { Version = 2.0, Id = 999, AccountCode = "N10101", AccountName = "New Account", IsActive = true });
            });
        }

    }
}
