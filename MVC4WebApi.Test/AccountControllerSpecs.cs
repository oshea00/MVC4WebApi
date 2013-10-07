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

            controller = new AccountController(accountRepo,null);
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
    public class AccountControllerPagingAndSortingAndSearching
    {
        IAccountRepo accountRepo;
        AccountController controller;

        [SetUp]
        public void Setup()
        {
            accountRepo = Substitute.For<IAccountRepo>();

            accountRepo.GetAll().Returns(new List<Account> { 
               new Account { Id = 1, AccountCode = "A9", Name = "A", Balance = 10000.00, BalanceDate = new DateTime(2013,09,1), IsActive = true },
               new Account { Id = 2, AccountCode = "A8", Name = "B", Balance = 1000.00, BalanceDate = new DateTime(2013,09,2), IsActive = true },
               new Account { Id = 3, AccountCode = "A7", Name = "C", Balance = 100.00, BalanceDate = new DateTime(2013,09,3), IsActive = false },
               new Account { Id = 4, AccountCode = "A6", Name = "D", Balance = 10.00, BalanceDate = new DateTime(2013,09,4), IsActive = true },
               new Account { Id = 5, AccountCode = "A5", Name = "E", Balance = 1.00, BalanceDate = new DateTime(2013,09,5), IsActive = false },
               });

            controller = new AccountController(accountRepo,null);
            controller.Request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/Account");
            controller.Configuration = GlobalConfiguration.Configuration;
            controller.Version = 2.0;
        }

        [Test]
        public void returnsMatchOnBools()
        {
            var accountModel = controller.GetBySearch("false").First();
            Assert.AreEqual("A7", accountModel.AccountCode);
            accountModel = controller.GetBySearch("true").First();
            Assert.AreEqual("A9", accountModel.AccountCode);
        }

        [Test]
        public void returnsMatchOnNumbers()
        {
            var accountModel = controller.GetBySearch("1.00").First();
            Assert.AreEqual("A5", accountModel.AccountCode);
            accountModel = controller.GetBySearch("100.").First();
            Assert.AreEqual("A7", accountModel.AccountCode);
        }

        [Test]
        public void returnsMatcheOnStrings()
        {
            var accountModel = controller.GetBySearch("a9").First();
            Assert.AreEqual("A9", accountModel.AccountCode);
            accountModel = controller.GetBySearch("D").First();
            Assert.AreEqual("A6", accountModel.AccountCode);
        }

        [Test]
        public void returnsMatcheOnDates()
        {
            var accountModel = controller.GetBySearch("09/01/2013").First();
            Assert.AreEqual(new DateTime(2013,9,1), accountModel.BalanceDate);
            accountModel = controller.GetBySearch("9/1/2013").First();
            Assert.AreEqual(new DateTime(2013, 9, 1), accountModel.BalanceDate);
            accountModel = controller.GetBySearch("9/1").First();
            Assert.AreEqual(new DateTime(2013, 9, 1), accountModel.BalanceDate);
        }

        [Test]
        public void returnsOrderedByAccountCode()
        {
            var accountModel = controller.GetOrderBy(0,5,"AccountCode").First();
            Assert.AreEqual("A5",accountModel.AccountCode);
            accountModel = controller.GetOrderBy(0, 5, "-AccountCode").First();
            Assert.AreEqual("A9", accountModel.AccountCode);
        }

        [Test]
        public void returnsOrderedByAccountName()
        {
            var accountModel = controller.GetOrderBy(0, 5, "AccountName").First();
            Assert.AreEqual("A", accountModel.AccountName);
            accountModel = controller.GetOrderBy(0, 5, "-AccountName").First();
            Assert.AreEqual("E", accountModel.AccountName);
        }

        [Test]
        public void returnsOrderedByBalance()
        {
            var accountModel = controller.GetOrderBy(0, 5, "Balance").First();
            Assert.AreEqual(1.0, accountModel.Balance);
            accountModel = controller.GetOrderBy(0, 5, "-Balance").First();
            Assert.AreEqual(10000.0, accountModel.Balance);
        }

        [Test]
        public void returnsOrderedByBalanceDate()
        {
            var accountModel = controller.GetOrderBy(0, 5, "BalanceDate").First();
            Assert.AreEqual(1, accountModel.BalanceDate.Day);
            accountModel = controller.GetOrderBy(0, 5, "-BalanceDate").First();
            Assert.AreEqual(5, accountModel.BalanceDate.Day);
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

            controller = new AccountController(accountRepo,null);
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
