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

namespace MVC4WebApi.Test
{

    [TestFixture]
    public class AccountControllerSpecs
    {
        IAccountRepo accountRepo;
        AccountController controller;
        MVC4WebApi.Models.Account modelAccount;
        MVC4WebApi.Domain.Account existingDomainAccount;
        MVC4WebApi.Domain.Account newDomainAccount;

        [SetUp]
        public void Setup()
        {
            accountRepo = Substitute.For<IAccountRepo>();

            accountRepo.GetAll().Returns(new List<Account> { 
                 new Account { Id = 1},
            });

            modelAccount = new MVC4WebApi.Models.Account { Version = 2.0, Id = 1, AccountCode = "U10101", AccountName = "Updated Account", IsActive = true };
            existingDomainAccount = modelAccount.AccountMap(version: 2.0);
            newDomainAccount = new MVC4WebApi.Domain.Account { Id=2, AccountCode = "N101010", Name = "New Account", IsActive = true };
            accountRepo.Get(1).Returns(existingDomainAccount);
            accountRepo.Get(2).Returns(newDomainAccount);
            accountRepo.Add(Arg.Any<MVC4WebApi.Domain.Account>()).Returns(newDomainAccount);
            accountRepo.Update(existingDomainAccount).Returns(true);

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
            //controller.PutAccount(modelAccount.Id,modelAccount);
            //accountRepo.Received().Add(Arg.Any<MVC4WebApi.Domain.Account>());

        }

        [Test]
        public void savesAnNewAccount()
        {
            //var newModelAccount = newDomainAccount.AccountMap(version: 2.0);
            //controller.PostAccount(newModelAccount);
            //accountRepo.Received().Add(Arg.Any<MVC4WebApi.Domain.Account>());
        }
    }
}
