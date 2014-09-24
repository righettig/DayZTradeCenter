using System;
using System.Linq;
using System.Web.Mvc;
using DayZTradeCenter.DomainModel;
using rg.GenericRepository.Core;

namespace DayZTradeCenter.UI.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="tradesRepository">The trades repository.</param>
        /// <exception cref="System.ArgumentNullException">tradesRepository</exception>
        public HomeController(IRepository<Trade> tradesRepository)
        {
            if (tradesRepository == null)
            {
                throw new ArgumentNullException("tradesRepository");
            }

            _tradesRepository = tradesRepository;
        }

        public ActionResult Index()
        {
            var latestTrade =
                _tradesRepository
                    .GetAll()
                    .OrderByDescending(trade => trade.CreationDate)
                    .FirstOrDefault();

            return View(latestTrade);
        }

        private readonly IRepository<Trade> _tradesRepository;
    }
}