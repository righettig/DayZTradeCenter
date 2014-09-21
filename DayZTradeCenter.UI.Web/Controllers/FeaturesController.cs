using System.Web.Mvc;

namespace DayZTradeCenter.UI.Web.Controllers
{
    [AllowAnonymous]
    public class FeaturesController : Controller
    {
        // GET: Features
        public ActionResult Index()
        {
            return View();
        }
    }
}