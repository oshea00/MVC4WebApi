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

namespace MVC4WebApi.Test
{

    [TestFixture]
    public class AccountControllerSaveandUpdateGoodPathSpecs
    {
        IAccountRepo accountRepo;
        AccountController controller;
        MVC4WebApi.Models.Account existingModelAccount;
        MVC4WebApi.Models.Account newModelAccount;
        MVC4WebApi.Domain.Account existingDomainAccount;
        MVC4WebApi.Domain.Account newDomainAccount;

        [SetUp]
        public void Setup()
        {
            accountRepo = Substitute.For<IAccountRepo>();

            accountRepo.GetAll().Returns(new List<Account> { 
                 new Account { Id = 1},
            });

            existingModelAccount = new MVC4WebApi.Models.Account { Version = 2.0, Id = 1, AccountCode = "U10101", AccountName = "Updated Account", IsActive = true };
            newModelAccount = new MVC4WebApi.Models.Account { Version = 2.0, Id = 0, AccountCode = "N10101", AccountName = "New Account", IsActive = true };

            accountRepo.Get(1).Returns(existingModelAccount.AccountMap(2.0));
            accountRepo.Add(Arg.Any<MVC4WebApi.Domain.Account>()).Returns(newModelAccount.AccountMap(2.0));
            accountRepo.Update(Arg.Any<MVC4WebApi.Domain.Account>()).Returns(true);

            controller = new AccountController(accountRepo);
            controller.Request = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, "http://localhost/api/Account");
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

            accountRepo.Update(Arg.Any<MVC4WebApi.Domain.Account>()).Returns(false);

            controller = new AccountController(accountRepo);
            controller.Request = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, "http://localhost/api/Account");
            controller.Configuration = GlobalConfiguration.Configuration;
            controller.Version = 2.0;
        }

        [Test]
        public void UpdatingNonExistentAccountIsAnError()
        {
            Assert.Throws<HttpResponseException>(() =>
            {
                controller.PostAccount(new MVC4WebApi.Models.Account { Id = 999 });
            });
        }

    }
}
