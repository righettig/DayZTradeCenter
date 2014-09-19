using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DayZTradeCenter.UI.Web.Startup))]
namespace DayZTradeCenter.UI.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
