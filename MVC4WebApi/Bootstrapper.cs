using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MVC4WebApi.Domain.Data;
using Unity.Mvc3;

namespace MVC4WebApi
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();         
            container.RegisterType<IAccountRepo, AccountRepo>();

            return container;
        }
    }
}