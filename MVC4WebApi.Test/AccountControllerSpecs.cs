using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC4WebApi.Controllers;
using MVC4WebApi.Domain.Data;
using MVC4WebApi.Domain;
using NSubstitute;
using NUnit.Framework;

namespace MVC4WebApi.Test
{

    [TestFixture]
    public class AccountControllerSpecs
    {
        IAccountRepo accountRepo;
        AccountController controller;

        [SetUp]
        public void Setup()
        {
            accountRepo = Substitute.For<IAccountRepo>();

            accountRepo.getAll().Returns(new List<Account> { 
                 new Account { Id = 1},
            });

            accountRepo.getById(1).Returns(new Account { Id = 1});

            accountRepo.Save(new Account { Id = 0 }).Returns( 2 );

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
        public void savesAnAccount()
        {

        }
    }
}
