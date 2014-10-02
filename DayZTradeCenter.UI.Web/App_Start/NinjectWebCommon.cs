using DayZTradeCenter.DomainModel;
using DayZTradeCenter.DomainModel.Identity;
using DayZTradeCenter.DomainModel.Identity.Entities;
using DayZTradeCenter.DomainModel.Interfaces;
using DayZTradeCenter.Modules.Test;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(DayZTradeCenter.UI.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(DayZTradeCenter.UI.Web.App_Start.NinjectWebCommon), "Stop")]

namespace DayZTradeCenter.UI.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            // TODO: the app still needs to know in advance where the modules are.
            kernel.Load<TradesRepositoryInMemoryModule>();
            kernel.Load<ItemsRepositoryInMemoryModule>();
            kernel.Load<EventInfoRepositoryInMemoryModule>();

            kernel.Bind<ITradeManager>().To<TradeManager>().InRequestScope();
            kernel.Bind<IProfileManager>().To<ProfileManager>().InRequestScope();

            kernel.Bind<IUserStore<ApplicationUser>>().ToMethod(
                c =>
                    new UserStore<ApplicationUser>(
                        HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>()));
        }        
    }
}
