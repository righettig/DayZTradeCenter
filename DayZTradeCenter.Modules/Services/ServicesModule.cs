using DayZTradeCenter.DomainModel.Interfaces;
using DayZTradeCenter.DomainModel.Services;
using Ninject.Modules;
using Ninject.Web.Common;

namespace DayZTradeCenter.Modules.Services
{
    public class ServicesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITradeManager>().To<TradeManager>().InRequestScope();
            Bind<IProfileManager>().To<ProfileManager>().InRequestScope();
            Bind<IAnalyticsProvider>().To<AnalyticsProvider>().InRequestScope();

            Bind<ApplicationUserManager>().ToSelf().InRequestScope();
            Bind<ApplicationSignInManager>().ToSelf().InRequestScope();
        }
    }
}
