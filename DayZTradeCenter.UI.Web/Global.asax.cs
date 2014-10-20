using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DayZTradeCenter.DomainModel.Entities.Messages;
using DayZTradeCenter.UI.Web.Helpers;

namespace DayZTradeCenter.UI.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
#if DEBUG
            // registers the custom model binder for "ExchangeDetails".
            // This fixes the issue with dates like 10/20/2014 that are wrongly interpreted when run outside "en" culture.
            ModelBinders.Binders[typeof (ExchangeDetails)] = new ExchangeDetailsModelBinder();
#endif
        }
    }
}
