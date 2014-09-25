using System;
using System.Linq;
using System.Web.Mvc;
using DayZTradeCenter.DomainModel;

namespace DayZTradeCenter.UI.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="tradeManager">The trade manager.</param>
        /// <exception cref="System.ArgumentNullException">tradeManager</exception>
        public HomeController(ITradeManager tradeManager)
        {
            if (tradeManager == null)
            {
                throw new ArgumentNullException("tradeManager");
            }

            _tradeManager = tradeManager;
        }

        public ActionResult Index()
        {
            var model = _tradeManager.GetLatestTrades(1).FirstOrDefault();

            return View(model);
        }

        private readonly ITradeManager _tradeManager;
    }
}