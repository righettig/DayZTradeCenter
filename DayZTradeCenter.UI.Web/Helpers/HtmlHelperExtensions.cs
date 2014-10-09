using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace DayZTradeCenter.UI.Web.Helpers
{
    // http://stackoverflow.com/questions/12694267/print-version-number-in-asp-net-mvc-4-app
    public static class HtmlHelperExtensions
    {
        public static IHtmlString AssemblyVersion(this HtmlHelper helper)
        {
            var version = 
                Assembly.GetExecutingAssembly().GetName().Version.ToString();

            return MvcHtmlString.Create(version);
        }
    }
}