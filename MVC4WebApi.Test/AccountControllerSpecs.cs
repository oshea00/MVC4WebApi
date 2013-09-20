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

            accountRepo.getAll().Returns(new List<Account> { 
                 new Account { Id = 1},
            });

            modelAccount = new MVC4WebApi.Models.Account { Version = 2.0, Id = 1, AccountCode = "U10101", AccountName = "Updated Account", IsActive = true };
            existingDomainAccount = modelAccount.AccountMap(version: 2.0);
            newDomainAccount = new MVC4WebApi.Domain.Account { Id=0, AccountCode = "N101010", Name = "New Account", IsActive = true };
            accountRepo.getById(1).Returns(existingDomainAccount);
            accountRepo.getById(2).Returns(newDomainAccount);
            accountRepo.Save(newDomainAccount).Returns(2);

            controller = new AccountController(accountRepo);
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
            controller.Put(modelAccount.Id,modelAccount);
            accountRepo.Received().Save(Arg.Any<MVC4WebApi.Domain.Account>());

        }

        [Test]
        public void savesAnNewAccount()
        {
            var newModelAccount = newDomainAccount.AccountMap(version: 2.0);
            controller.Post(newModelAccount);
            accountRepo.Received().Save(Arg.Any<MVC4WebApi.Domain.Account>());
        }
    }
}
