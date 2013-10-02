using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using MVC4WebApi.Controllers;
using NSubstitute;
using NUnit.Framework;

namespace MVC4WebApi.Test
{
    [TestFixture]
    public class BaseApiControllerHandlesVersionHeaderGreaterThanOne
    {
        BaseApiController controller = null;
        HttpControllerContext context = null;

        [SetUp]
        public void Setup()
        {
            context = new HttpControllerContext();
            context.Configuration = new System.Web.Http.HttpConfiguration();
            context.Request = new HttpRequestMessage(HttpMethod.Get, "/Account");
            context.Request.Headers.Add("X-Version", "2.0");
            controller = new BaseApiController();
            try {
                var response = controller.ExecuteAsync(context, new CancellationToken());
            }
            catch { }
        }

        [Test]
        public void SetsVersionBasedOnRequestHeaderTo2()
        {
            Assert.AreEqual(controller.Version, 2.0);
        }
    }

    [TestFixture]
    public class BaseApiControllerHandlesNoVersionHeader
    {
        BaseApiController controller = null;
        HttpControllerContext context = null;

        [SetUp]
        public void Setup()
        {
            context = new HttpControllerContext();
            context.Configuration = new System.Web.Http.HttpConfiguration();
            context.Request = new HttpRequestMessage(HttpMethod.Get, "/Account");
            controller = new BaseApiController();
            try
            {
                var response = controller.ExecuteAsync(context, new CancellationToken());
            }
            catch { }
        }

        [Test]
        public void SetsVersionBasedOnNoVersionRequestHeaderTo1()
        {
            Assert.AreEqual(controller.Version, 1.0);
        }
    }

    [TestFixture]
    public class BaseApiControllerHandlesBadVersionHeader
    {
        BaseApiController controller = null;
        HttpControllerContext context = null;

        [SetUp]
        public void Setup()
        {
            context = new HttpControllerContext();
            context.Configuration = new System.Web.Http.HttpConfiguration();
            context.Request = new HttpRequestMessage(HttpMethod.Get, "/Account");
            context.Request.Headers.Add("X-Version", "2a.0");
            controller = new BaseApiController();
            try
            {
                var response = controller.ExecuteAsync(context, new CancellationToken());
            }
            catch { }
        }

        [Test]
        public void SetsVersionBasedOnRequestHeaderTo1()
        {
            Assert.AreEqual(controller.Version, 1.0);
        }
    }

}
