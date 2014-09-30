using System.Web.Mvc;

namespace DayZTradeCenter.UI.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
            filters.Add(new LoadUserInboxActionFilter());
        }
    }
}
