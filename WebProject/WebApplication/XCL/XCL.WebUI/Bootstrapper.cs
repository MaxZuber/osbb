using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using XCL.Common.InversionofControl;
using XCL.Core.Services.Abstract;
using XCL.Core.Services.Impl;
using XCL.Repository.Repositories.Abstract;
using XCL.Repository.Repositories.Impl;
using XCL.WebUI.IoC;

namespace XCL.WebUI
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise(HttpConfiguration httpConfig)
        {
            var container = BuildUnityContainer();

            var dependencyResolver = new UnityResolver(container);
            DependencyResolver.SetResolver(dependencyResolver);
            httpConfig.DependencyResolver = dependencyResolver;

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = Ioc.Instance;

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();    
            RegisterTypes(container);
            RegisterServices(container);
            RegisterRepositories(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
        }

        public static void RegisterServices(IUnityContainer container)
        {
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IEmailService, EmailService>();
            container.RegisterType<ICryptService, CryptService>();
            container.RegisterType<ISecurityService, SecurityService>();
            container.RegisterType<IBuildingService, BuildingService>();
            container.RegisterType<IRandomValuesService, RandomValuesService>();
            container.RegisterType<ISensorService, SensorService>();
            container.RegisterType<IEntranceService, EntranceService>();

        }

        public static void RegisterRepositories(IUnityContainer container)
        {
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IBuildingRepository, BuildingRepository>();
            container.RegisterType<ISensorRepositoy, SensorRepositoy>();
            container.RegisterType<IRepository, Repository.Repositories.Impl.Repository>();
        }
    }
}