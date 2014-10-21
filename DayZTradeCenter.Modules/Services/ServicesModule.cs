using DayZTradeCenter.DomainModel.Interfaces;
using DayZTradeCenter.DomainModel.Services;
using DayZTradeCenter.DomainModel.Services.Messaging;
using Microsoft.AspNet.Identity;
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

            Bind<IUserManager>().To<ApplicationUserManager>().InRequestScope();

            //var mailgunApiKey = "<< PUT YOUR DEV MAILGUN KEY HERE >>";

            //Bind<IIdentityMessageService>().To<MailgunWebApiEmailService>().InRequestScope()
            //    .WithConstructorArgument(mailgunApiKey);

#if DEBUG
            Bind<IIdentityMessageService>().To<LocalEmailService>().InRequestScope();
#else
            Bind<IIdentityMessageService>().To<SmtpEmailService>().InRequestScope();
#endif
        }
    }
}
