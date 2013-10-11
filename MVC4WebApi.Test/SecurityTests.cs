using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC4WebApi.Controllers;
using NSubstitute;
using NUnit.Framework;

namespace MVC4WebApi.Test
{
    [TestFixture]
    public class SecurityTests
    {
        [Test]
        public void UserProviderReturnsCurrentUser()
        {
            var userP = Substitute.For<IUserProvider>();
            userP.UserName.Returns("someuser");
            Assert.AreEqual("someuser", userP.UserName);
        }
    }
}
