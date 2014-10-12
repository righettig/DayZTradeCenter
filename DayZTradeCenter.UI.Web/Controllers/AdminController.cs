using System.Web.Mvc;

namespace DayZTradeCenter.UI.Web.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles = "Administrator")]
        public ViewResult Ban(string userId)
        {
            return View();
        }
    }
}